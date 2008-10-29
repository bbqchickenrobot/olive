// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//
// Copyright (c) 2007 Novell, Inc. (http://www.novell.com)
//
// Authors:
//	Chris Toshok (toshok@ximian.com)
//

using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using zipsharp;

namespace System.IO.Packaging {

	public sealed class ZipPackage : Package
	{
		private const string ContentNamespace = "http://schemas.openxmlformats.org/package/2006/content-types";
		private const string ContentUri = "[Content_Types].xml";
		
		Dictionary<Uri, ZipPackagePart> parts;
		internal Dictionary<Uri, MemoryStream> PartStreams = new Dictionary<Uri, MemoryStream> ();

		internal Stream PackageStream { get; set; }

		Dictionary<Uri, ZipPackagePart> Parts {
			get {
				if (parts == null)
					LoadParts ();
				return parts;
			}
		}
		internal ZipPackage (FileAccess access)
			: base (access)
		{

		}

		internal ZipPackage (FileAccess access, bool streaming)
			: base (access, streaming)
		{

		}
		
		protected override void Dispose (bool disposing)
		{
			foreach (Stream s in PartStreams.Values)
				s.Close ();
			
			PackageStream.Close ();
		}

		protected override void FlushCore ()
		{
			// Ensure that all the data has been read out of the package
			// stream already. Otherwise we'll lose data when we recreate the zip
			foreach (ZipPackagePart part in Parts.Values)
				part.GetStream ().Dispose ();

			// Empty the package stream
			PackageStream.Position = 0;
			PackageStream.SetLength (0);

			// Recreate the zip file
			using (ZipArchive archive = new ZipArchive(PackageStream, Append.Create, false)) {

				// Write all the part streams
				foreach (ZipPackagePart part in Parts.Values) {
					Console.WriteLine ("Flushing: {0}", part.Uri);
					Stream partStream = part.GetStream ();
					partStream.Seek (0, SeekOrigin.Begin);
					
					using (Stream destination = archive.GetStream (part.Uri.ToString ())) {
						int count = (int) Math.Min (2048, partStream.Length);
						byte[] buffer = new byte [count];

						while ((count = partStream.Read (buffer, 0, buffer.Length)) != 0)
							destination.Write (buffer, 0, count);
					}
				}

				using (Stream s = archive.GetStream (ContentUri))
					WriteContentType (s);
			}
		}

		protected override PackagePart CreatePartCore (Uri partUri, string contentType, CompressionOption compressionOption)
		{
			Console.WriteLine ("Creating Part: {0}", partUri);
			ZipPackagePart part = new ZipPackagePart (this, partUri, contentType, compressionOption);
			Parts.Add (part.Uri, part);
			return part;
		}

		protected override void DeletePartCore (Uri partUri)
		{
			Console.WriteLine ("Deleting Part: {0}", partUri);
			Parts.Remove (partUri);
		}

		protected override PackagePart GetPartCore (Uri partUri)
		{
			Console.WriteLine ("Getting Part: {0}", partUri);
			ZipPackagePart part = null;
			Parts.TryGetValue (partUri, out part);
			return part;
		}

		protected override PackagePart[] GetPartsCore ()
		{
			ZipPackagePart[] p = new ZipPackagePart [Parts.Count];
			Parts.Values.CopyTo (p, 0);
			return p;
		}
		
		void LoadParts ()
		{
			Console.WriteLine ("Loading parts");
			parts = new Dictionary<Uri, ZipPackagePart> ();
			try {
				using (UnzipArchive archive = new UnzipArchive (PackageStream)) {

					// Load the content type map file
					XmlDocument doc = new XmlDocument ();
					using (Stream s = archive.GetStream (ContentUri))
						doc.Load (s);
					
					XmlNamespaceManager manager = new XmlNamespaceManager (doc.NameTable);
					manager.AddNamespace ("content", ContentNamespace);

					foreach (string file in archive.GetFiles ()) {
						XmlNode node;

						Console.WriteLine ("Found file: {0}", file);
						
						if (file == RelationshipUri.ToString ())
						{
							CreatePart (RelationshipUri, RelationshipContentType);
							continue;
						}

						string xPath = string.Format ("/content:Types/content:Override[@PartName='{0}']", file);
						node = doc.SelectSingleNode (xPath, manager);

						if (node == null)
						{
							string ext = Path.GetExtension (file);
							xPath = string.Format("/content:Types/content:Default[@Extension='{0}']", ext);
							node = doc.SelectSingleNode (xPath, manager);
						}

						// What do i do if the node is null? This means some has tampered with the
						// package file manually
						if (node != null)
							CreatePart (new Uri (file, UriKind.Relative), node.Attributes["ContentType"].Value);
					}
				}
			} catch {
				Console.WriteLine ("The package was invalid...");
				// The archive is invalid - therfefore no parts
			}
		}

		void WriteContentType (Stream s)
		{
			XmlDocument doc = new XmlDocument ();
			XmlNamespaceManager manager = new XmlNamespaceManager (doc.NameTable);
			manager.AddNamespace ("content", ContentNamespace);

			doc.AppendChild(doc.CreateNode (XmlNodeType.XmlDeclaration, "", ""));
			
			XmlNode root = doc.CreateNode (XmlNodeType.Element, "Types", ContentNamespace);
			doc.AppendChild (root);
			foreach (ZipPackagePart part in Parts.Values)
			{
				XmlNode node = doc.CreateNode (XmlNodeType.Element, "Override", ContentNamespace);
				
				XmlAttribute contentType = doc.CreateAttribute ("ContentType");
				contentType.Value = part.ContentType;
				
				XmlAttribute name = doc.CreateAttribute ("PartName");
				name.Value = part.Uri.ToString ();
				

				node.Attributes.Append (contentType);
				node.Attributes.Append (name);

				root.AppendChild (node);				
			}

			using (XmlTextWriter writer = new XmlTextWriter (s, System.Text.Encoding.UTF8))
				doc.WriteTo (writer);
		}
	}
}
