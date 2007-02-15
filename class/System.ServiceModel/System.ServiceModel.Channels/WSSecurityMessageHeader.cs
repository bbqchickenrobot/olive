﻿//
// SecurityChannelFactory.cs
//
// Author:
//	Atsushi Enomoto  <atsushi@ximian.com>
//
// Copyright (C) 2006 Novell, Inc (http://www.novell.com)
//
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

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.Runtime.Serialization;
using System.Security.Cryptography.Xml;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Security;
using System.ServiceModel.Security.Tokens;
using System.Xml;

namespace System.ServiceModel.Channels
{
	internal class WSSecurityMessageHeader : MessageHeader
	{
		public static WSSecurityMessageHeader Read (XmlDictionaryReader reader, SecurityTokenSerializer serializer, SecurityTokenResolver resolver)
		{
			WSSecurityMessageHeader ret = new WSSecurityMessageHeader (serializer);

			reader.MoveToContent ();
			reader.ReadStartElement ("Security", Constants.WssNamespace);
			XmlDocument doc = new XmlDocument ();
			do {
				reader.MoveToContent ();
				if (reader.NodeType == XmlNodeType.EndElement)
					break;
				if (reader.NodeType != XmlNodeType.Element)
					throw new XmlException (String.Format ("Node type {0} is not expected as a WS-Security message header content.", reader.NodeType));
				switch (reader.NamespaceURI) {
				case Constants.WsuNamespace:
					switch (reader.LocalName) {
					case "Timestamp":
						ret.Contents.Add (ReadTimestamp (reader));
						continue;
					}
					break;
				case Constants.WsscNamespace:
					switch (reader.LocalName) {
					case "DerivedKeyToken":
						// FIXME: actually DerivedKeyToken should be read via WSSecurityTokenSerializer, but o
						ret.Contents.Add (ReadDerivedKeyToken (reader, doc, serializer));
						continue;
					}
					break;
				//case Constants.WstNamespace:
				case Constants.Wss11Namespace:
					if (reader.LocalName == "SignatureConfirmation") {
						ret.Contents.Add (ReadSignatureConfirmation (reader, doc));
						continue;
					}
					break;
				case SignedXml.XmlDsigNamespaceUrl:
					switch (reader.LocalName) {
					case "Signature":
						Signature sig = new Signature ();
						sig.LoadXml ((XmlElement) doc.ReadNode (reader));
						UpdateSignature (sig, doc, serializer);
						ret.Contents.Add (sig);
						continue;
					}
					break;
				case EncryptedXml.XmlEncNamespaceUrl:
					switch (reader.LocalName) {
					case "EncryptedData":
						EncryptedData ed = new EncryptedData ();
						ed.LoadXml ((XmlElement) doc.ReadNode (reader));
						ret.Contents.Add (ed);
						continue;
					case "ReferenceList":
						ReferenceList rl = new ReferenceList ();
						reader.Read ();
						for (reader.MoveToContent ();
						     reader.NodeType != XmlNodeType.EndElement;
						     reader.MoveToContent ()) {
							switch (reader.LocalName) {
							case "DataReference":
								DataReference dref = new DataReference ();
								dref.LoadXml ((XmlElement) doc.ReadNode (reader));
								rl.Add (dref);
								continue;
							case "KeyReference":
								KeyReference kref = new KeyReference ();
								kref.LoadXml ((XmlElement) doc.ReadNode (reader));
								rl.Add (kref);
								continue;
							}
							throw new XmlException (String.Format ("Unexpected {2} node '{0}' in namespace '{1}' in ReferenceList.", reader.Name, reader.NamespaceURI, reader.NodeType));
						}
						reader.ReadEndElement ();
						ret.Contents.Add (rl);
						continue;
					}
					break;
				}
				// SecurityTokenReference will be handled here.
				// This order (Token->KeyIdentifierClause) is
				// important because WrappedKey could be read
				// in both context (but must be a token here).
				if (serializer.CanReadToken (reader))
					ret.Contents.Add (serializer.ReadToken (reader, resolver));
				else if (serializer.CanReadKeyIdentifierClause (reader))
					ret.Contents.Add (serializer.ReadKeyIdentifierClause (reader));
				else
					throw new XmlException (String.Format ("Unexpected element '{0}' in namespace '{1}' as a WS-Security message header content.", reader.Name, reader.NamespaceURI));
			} while (true);
			reader.ReadEndElement ();

			return ret;
		}

		internal static void UpdateSignature (Signature sig, XmlDocument doc, SecurityTokenSerializer serializer)
		{
			KeyInfo ki = new KeyInfo ();
			ki.Id = sig.KeyInfo.Id;
			foreach (KeyInfoClause kic in sig.KeyInfo) {
				KeyInfoNode kin = kic as KeyInfoNode;
				if (kin != null) {
					SecurityTokenReferenceKeyInfo r = new SecurityTokenReferenceKeyInfo (serializer, doc);
					r.LoadXml (kin.Value);
					ki.AddClause (r);
				}
				else
					ki.AddClause (kic);
			}
			sig.KeyInfo = ki;
		}

		static Wss11SignatureConfirmation ReadSignatureConfirmation (XmlReader reader, XmlDocument doc)
		{
			string id = reader.GetAttribute ("Id", Constants.WsuNamespace);
			string value = reader.GetAttribute ("Value");
			reader.Skip ();
			return new Wss11SignatureConfirmation (id, value);
		}

		static WsscDerivedKeyToken ReadDerivedKeyToken (XmlReader reader, XmlDocument doc, SecurityTokenSerializer serializer)
		{
			WsscDerivedKeyToken dkt = new WsscDerivedKeyToken ();
			dkt.Id = reader.GetAttribute ("Id", Constants.WsuNamespace);
			dkt.Algorithm = reader.GetAttribute ("Algorithm", String.Empty);
			reader.ReadStartElement ();
			
			for (reader.MoveToContent ();
			     reader.NodeType != XmlNodeType.EndElement;
			     reader.MoveToContent ()) {
				if (reader.NodeType != XmlNodeType.Element)
					throw new XmlException (String.Format ("Unexpected {0} node in DerivedKeyToken element.", reader.NodeType));
				switch (reader.NamespaceURI) {
				case Constants.WssNamespace:
					switch (reader.LocalName) {
					case "SecurityTokenReference":
						dkt.SecurityTokenReference = serializer.ReadKeyIdentifierClause (reader);
						continue;
					}
					break;
				case Constants.WsscNamespace:
					switch (reader.LocalName) {
					case "Length":
						dkt.Length = reader.ReadElementContentAsInt ();
						continue;
					case "Offset":
						dkt.Offset= reader.ReadElementContentAsInt ();
						continue;
					case "Nonce":
						dkt.Nonce = Convert.FromBase64String (reader.ReadElementContentAsString ());
						continue;
					}
					break;
				}
				throw new XmlException (String.Format ("Unexpected element in DerivedKeyToken element. Name is '{0}' and namespace URI is '{1}'.", reader.Name, reader.NamespaceURI));
			}
			reader.ReadEndElement ();
			return dkt;
		}

		static WsuTimestamp ReadTimestamp (XmlDictionaryReader reader)
		{
			WsuTimestamp ret = new WsuTimestamp ();
			ret.Id = reader.GetAttribute ("Id", Constants.WsuNamespace);
			reader.ReadStartElement ();
			do {
				reader.MoveToContent ();
				if (reader.NodeType == XmlNodeType.EndElement)
					break;
				if (reader.NodeType != XmlNodeType.Element)
					throw new XmlException (String.Format ("Node type {0} is not expected as a WS-Security 'Timestamp' content.", reader.NodeType));
				switch (reader.NamespaceURI) {
				case Constants.WsuNamespace:
					switch (reader.LocalName) {
					case "Created":
						ret.Created = (DateTime) reader.ReadElementContentAs (typeof (DateTime), null);
						continue;
					case "Expires":
						ret.Expires = (DateTime) reader.ReadElementContentAs (typeof (DateTime), null);
						continue;
					}
					break;
				}
				throw new XmlException (String.Format ("Unexpected element '{0}' in namespace '{1}' as a WS-Security message header content.", reader.Name, reader.NamespaceURI));
			} while (true);

			reader.ReadEndElement (); // </u:Timestamp>
			return ret;
		}

		public WSSecurityMessageHeader (SecurityTokenSerializer serializer)
		{
			this.serializer = serializer;
		}

		SecurityTokenSerializer serializer;
		Collection<object> contents = new Collection<object> ();

		// Timestamp, BinarySecurityToken, EncryptedKey,
		// [DerivedKeyToken]*, ReferenceList, EncryptedData
		public Collection<object> Contents {
			get { return contents; }
		}

		public override bool MustUnderstand {
			get { return true; }
		}

		public override string Name {
			get { return "Security"; }
		}

		public override string Namespace {
			get { return Constants.WssNamespace; }
		}

		public T Find<T> ()
		{
			foreach (object o in Contents)
				if (typeof (T).IsAssignableFrom (o.GetType ()))
					return (T) o;
			return default (T);
		}

		protected override void OnWriteStartHeader (XmlDictionaryWriter writer, MessageVersion version)
		{
			writer.WriteStartElement ("o", this.Name, this.Namespace);
			WriteHeaderAttributes (writer, version);
		}

		protected override void OnWriteHeaderContents (XmlDictionaryWriter writer, MessageVersion version)
		{
			foreach (object obj in Contents) {
				if (obj is WsuTimestamp) {
					WsuTimestamp ts = (WsuTimestamp) obj;
					ts.WriteTo (writer);
				} else if (obj is WsscDerivedKeyToken) {
					WsscDerivedKeyToken dk = (WsscDerivedKeyToken) obj;
					writer.WriteStartElement ("c", "DerivedKeyToken", Constants.WsscNamespace);
					if (dk.Id != null)
						writer.WriteAttributeString ("u", "Id", Constants.WsuNamespace, dk.Id);
					if (dk.Algorithm != null)
						writer.WriteAttributeString ("Algorithm", dk.Algorithm);
					serializer.WriteKeyIdentifierClause (writer, dk.SecurityTokenReference);
					writer.WriteStartElement ("Offset", Constants.WsscNamespace);
					writer.WriteValue (dk.Offset);
					writer.WriteEndElement ();

					writer.WriteStartElement ("Length", Constants.WsscNamespace);
					writer.WriteValue (dk.Length);
					writer.WriteEndElement ();

					writer.WriteStartElement ("Nonce", Constants.WsscNamespace);
					byte [] bytes = dk.Nonce;
					writer.WriteBase64 (bytes, 0, bytes.Length);
					writer.WriteEndElement ();

					writer.WriteEndElement ();
				} else if (obj is SecurityToken) {
					serializer.WriteToken (writer, (SecurityToken) obj);
				} else if (obj is EncryptedKey) {
					((EncryptedKey) obj).GetXml ().WriteTo (writer);
				} else if (obj is ReferenceList) {
					writer.WriteStartElement ("ReferenceList", EncryptedXml.XmlEncNamespaceUrl);
					foreach (EncryptedReference er in (ReferenceList) obj)
						er.GetXml ().WriteTo (writer);
					writer.WriteEndElement ();
				} else if (obj is EncryptedData) {
					((EncryptedData) obj).GetXml ().WriteTo (writer);
				} else if (obj is Signature) {
					((Signature) obj).GetXml ().WriteTo (writer);
				} else if (obj is Wss11SignatureConfirmation) {
					Wss11SignatureConfirmation sc = (Wss11SignatureConfirmation) obj;
					writer.WriteStartElement ("k", "SignatureConfirmation", Constants.Wss11Namespace);
					writer.WriteAttributeString ("u", "Id", Constants.WsuNamespace, sc.Id);
					writer.WriteAttributeString ("Value", sc.Value);
					writer.WriteEndElement ();
				}
				else
					throw new ArgumentException (String.Format ("Unrecognized header item {0}", obj));
			}
		}
	}

	internal class WsuTimestamp
	{
		string id;
		DateTime created, expires;

		public string Id {
			get { return id; }
			set { id = value; }
		}

		public DateTime Created {
			get { return created; }
			set { created = value; }
		}

		public DateTime Expires {
			get { return expires; }
			set { expires = value; }
		}

		public void WriteTo (XmlWriter writer)
		{
			writer.WriteStartElement ("u", "Timestamp", Constants.WsuNamespace);
			writer.WriteAttributeString ("u", "Id", Constants.WsuNamespace, Id);
			writer.WriteStartElement ("u", "Created", Constants.WsuNamespace);
			writer.WriteValue (FormatAsUtc (Created));
			writer.WriteEndElement ();
			writer.WriteStartElement ("u", "Expires", Constants.WsuNamespace);
			writer.WriteValue (FormatAsUtc (Expires));
			writer.WriteEndElement ();
			writer.WriteEndElement ();
		}

		string FormatAsUtc (DateTime date)
		{
			return date.ToUniversalTime ().ToString (
				"yyyy-MM-dd'T'HH:mm:ss.fff'Z'",
				CultureInfo.InvariantCulture);
		}
	}

	internal class WsscDerivedKeyToken
	{
		public string Id;
		public string Algorithm;
		public SecurityKeyIdentifierClause SecurityTokenReference;
		public int Length;
		public int Offset;
		public byte [] Nonce;
	}

	internal class SecurityTokenReferenceKeyInfo : KeyInfoClause
	{
		SecurityKeyIdentifierClause clause;
		SecurityTokenSerializer serializer;
		XmlDocument doc;

		// for LoadXml()
		public SecurityTokenReferenceKeyInfo (
			SecurityTokenSerializer serializer,
			XmlDocument doc)
			: this (null, serializer, doc)
		{
		}

		// for GetXml()
		public SecurityTokenReferenceKeyInfo (
			SecurityKeyIdentifierClause clause,
			SecurityTokenSerializer serializer,
			XmlDocument doc)
		{
			this.clause = clause;
			this.serializer = serializer;
			if (doc == null)
				doc = new XmlDocument ();
			this.doc = doc;
		}

		public SecurityKeyIdentifierClause Clause {
			get { return clause; }
		}

		public override XmlElement GetXml ()
		{
			XmlDocumentFragment df = doc.CreateDocumentFragment ();
			XmlWriter w = df.CreateNavigator ().AppendChild ();
			serializer.WriteKeyIdentifierClause (w, clause);
			w.Close ();
			return (XmlElement) df.FirstChild;
		}

		public override void LoadXml (XmlElement element)
		{
			clause = serializer.ReadKeyIdentifierClause (new XmlNodeReader (element));
		}
	}

	internal class Wss11SignatureConfirmation
	{
		string id, value;

		public Wss11SignatureConfirmation (string id, string value)
		{
			this.id = id;
			this.value = value;
		}

		public string Id {
			get { return id; }
			set { id = value; }
		}

		public string Value {
			get { return value; }
			set { this.value = value; }
		}
	}
}
