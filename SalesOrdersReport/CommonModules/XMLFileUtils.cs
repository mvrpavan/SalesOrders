using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace SalesOrdersReport.CommonModules
{
    public class XMLFileUtils
    {
        public static Boolean GetAttributeValueFromXMLFile(String XMLFilePath, String AttributeXPath, out String Value)
        {
            Value = null;
            try
            {
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.Load(XMLFilePath);

                XmlNode AttributeNode = xmldoc.SelectSingleNode(AttributeXPath);
                if (AttributeNode == null) return false;

                Value = AttributeNode.Value;

                return true;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("XMLFileUtils.GetAttributeValueFromXMLFile", ex);
                return true;
            }
        }

        public static Boolean GetElementValueFromXMLFile(String XMLFilePath, String ElementXPath, out String Value)
        {
            Value = null;
            try
            {
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.Load(XMLFilePath);

                XmlNode ElementNode = xmldoc.SelectSingleNode(ElementXPath);
                if (ElementNode == null) return false;

                Value = ElementNode.Value;

                return true;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("XMLFileUtils.GetElementValueFromXMLFile", ex);
                return true;
            }
        }

        public static Boolean GetChildNodeValue(XmlNode Node, String ChildNodeName, out String Value)
        {
            Value = null;
            try
            {
                if (!Node.HasChildNodes) return false;

                foreach (XmlElement Element in Node.ChildNodes)
                {
                    if (Element.Name.Equals(ChildNodeName, StringComparison.InvariantCultureIgnoreCase))
                    {
                        if (String.IsNullOrEmpty(Element.InnerText)) return false;
                        Value = Element.InnerText;
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("XMLFileUtils.GetChildNodeValue", ex);
                return true;
            }
        }

        public static Boolean SetChildNodeValue(XmlNode Node, String ChildNodeName, String Value)
        {
            try
            {
                if (!Node.HasChildNodes) return false;

                foreach (XmlElement Element in Node.ChildNodes)
                {
                    if (Element.Name.Equals(ChildNodeName, StringComparison.InvariantCultureIgnoreCase))
                    {
                        //if (String.IsNullOrEmpty(Element.InnerText)) return false;
                        Element.InnerText = Value;
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("XMLFileUtils.SetChildNodeValue", ex);
                return true;
            }
        }

        public static XmlNode AddChildNodeToNodeRecusrive(XmlNode CurrentNode, String ChildNodeXPath)
        {
            try
            {
                String[] ChildNodeHierarchy = ChildNodeXPath.Split("/".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (ChildNodeHierarchy.Length == 0) return CurrentNode;
                if (ChildNodeHierarchy.Length == 1 && ChildNodeHierarchy[0].StartsWith("@")) return CurrentNode;

                XmlNode ChildNode = null;
                if (CurrentNode.Name.Equals(ChildNodeHierarchy[0], StringComparison.InvariantCultureIgnoreCase))
                {
                    ChildNode = CurrentNode;
                }

                if (ChildNode == null && CurrentNode.HasChildNodes)
                {
                    foreach (XmlNode item in CurrentNode.ChildNodes)
                    {
                        if (item.Name.Equals(ChildNodeHierarchy[0], StringComparison.InvariantCultureIgnoreCase))
                        {
                            ChildNode = item;
                            break;
                        }
                    }
                }

                if (ChildNode == null)
                {
                    ChildNode = CurrentNode.OwnerDocument.CreateElement(ChildNodeHierarchy[0]);
                    CurrentNode.AppendChild(ChildNode);
                }

                String GrandChildNodeXPath = "/";
                for (int i = 1; i < ChildNodeHierarchy.Length; i++)
                {
                    GrandChildNodeXPath += "/" + ChildNodeHierarchy[i];
                }

                return AddChildNodeToNodeRecusrive(ChildNode, GrandChildNodeXPath);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("XMLFileUtils.AddChildNodeToNodeRecusrive", ex);
                return null;
            }
        }

        public static Boolean SetAttributeValueInXMLFile(String XMLFilePath, String AttributeXPath, String Value, String SaveXMLFilePath = "")
        {
            try
            {
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.Load(XMLFilePath);

                Boolean AttributeModified = false;

                XmlNode AttributeNode = xmldoc.SelectSingleNode(AttributeXPath);
                if (AttributeNode == null)
                {
                    XmlAttribute Attribute = xmldoc.CreateAttribute(AttributeXPath.Substring(AttributeXPath.LastIndexOf('@') + 1));
                    Attribute.Value = Value;

                    String RootNodeName = AttributeXPath.Split("/".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)[0];
                    XmlNode AttributeParentNode = AddChildNodeToNodeRecusrive(xmldoc.SelectSingleNode("//" + RootNodeName), AttributeXPath.Substring(0, AttributeXPath.LastIndexOf('/')));
                    AttributeParentNode.Attributes.Append(Attribute);
                    AttributeModified = true;
                }
                else
                {
                    if (!AttributeNode.Value.Equals(Value))
                    {
                        AttributeNode.Value = Value;
                        AttributeModified = true;
                    }
                }

                if (AttributeModified)
                {
                    if (!String.IsNullOrEmpty(SaveXMLFilePath))
                        xmldoc.Save(SaveXMLFilePath);
                    else
                        xmldoc.Save(XMLFilePath);
                }

                return true;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("XMLFileUtils.SetAttributeValueInXMLFile", ex);
                return false;
            }
        }

        public static Boolean SetAttributeValue(XmlNode Node, String AttributeName, String Value)
        {
            try
            {
                XmlAttribute AttributeElement = null;
                if (Node.Attributes != null)
                {
                    foreach (XmlAttribute item in Node.Attributes)
                    {
                        if (item.Name.Equals(AttributeName, StringComparison.InvariantCultureIgnoreCase))
                        {
                            AttributeElement = item;
                            break;
                        }
                    }
                }

                if (AttributeElement == null)
                {
                    AttributeElement = Node.OwnerDocument.CreateAttribute(AttributeName);
                    Node.Attributes.Append(AttributeElement);
                }
                AttributeElement.Value = Value;

                return true;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("XMLFileUtils.SetAttributeValue", ex);
                return false;
            }
        }

        public static Boolean SetElementValueInXMLFile(String XMLFilePath, String ElementXPath, String Value, String SaveXMLFilePath = "")
        {
            try
            {
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.Load(XMLFilePath);

                Boolean ElementModified = false;

                XmlNode ElementNode = xmldoc.SelectSingleNode(ElementXPath);
                if (ElementNode == null)
                {
                    String RootNodeName = ElementXPath.Split("/".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)[0];
                    ElementNode = AddChildNodeToNodeRecusrive(xmldoc.SelectSingleNode("//" + RootNodeName), ElementXPath);
                    ElementNode.InnerText = Value;
                    ElementModified = true;
                }
                else
                {
                    if (!ElementNode.InnerText.Equals(Value))
                    {
                        ElementNode.InnerText = Value;
                        ElementModified = true;
                    }
                }

                if (ElementModified)
                {
                    if (!String.IsNullOrEmpty(SaveXMLFilePath))
                        xmldoc.Save(SaveXMLFilePath);
                    else
                        xmldoc.Save(XMLFilePath);
                }

                return true;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("XMLFileUtils.SetElementValueInXMLFile", ex);
                return false;
            }
        }

        public static Boolean GetChildNode(XmlNode Node, String NodeName, out XmlNode ChildNode)
        {
            ChildNode = null;
            try
            {
                foreach (XmlNode node in Node.ChildNodes)
                {
                    if (node.NodeType != XmlNodeType.Comment && node.Name.Equals(NodeName, StringComparison.InvariantCultureIgnoreCase))
                    {
                        ChildNode = node;
                        return true;
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("XMLFileUtils.GetChildNode", ex);
                return false;
            }
        }

        public static Boolean GetAttributeValue(XmlNode Node, String AttributeName, out Boolean Value)
        {
            Value = false;
            try
            {
                foreach (XmlAttribute Attribute in Node.Attributes)
                {
                    if (Attribute.Name.Equals(AttributeName, StringComparison.InvariantCultureIgnoreCase))
                    {
                        if (String.IsNullOrEmpty(Attribute.Value)) return false;
                        Value = Boolean.Parse(Attribute.Value);
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("XMLFileUtils.GetAttributeValue-Boolean", ex);
                return false;
            }
        }

        public static Boolean GetAttributeValue(XmlNode Node, String AttributeName, out Int32 Value)
        {
            Value = Int32.MinValue;
            try
            {
                foreach (XmlAttribute Attribute in Node.Attributes)
                {
                    if (Attribute.Name.Equals(AttributeName, StringComparison.InvariantCultureIgnoreCase))
                    {
                        if (String.IsNullOrEmpty(Attribute.Value)) return false;
                        Value = Int32.Parse(Attribute.Value);
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("XMLFileUtils.GetAttributeValue-Int32", ex);
                return false;
            }
        }

        public static Boolean GetAttributeValue(XmlNode Node, String AttributeName, out Double Value)
        {
            Value = Double.MinValue;
            try
            {
                foreach (XmlAttribute Attribute in Node.Attributes)
                {
                    if (Attribute.Name.Equals(AttributeName, StringComparison.InvariantCultureIgnoreCase))
                    {
                        if (String.IsNullOrEmpty(Attribute.Value)) return false;
                        Value = Double.Parse(Attribute.Value);
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("XMLFileUtils.GetAttributeValue-Double", ex);
                return false;
            }
        }

        public static Boolean GetAttributeValue(XmlNode Node, String AttributeName, out String Value)
        {
            Value = "";
            try
            {
                foreach (XmlAttribute Attribute in Node.Attributes)
                {
                    if (Attribute.Name.Equals(AttributeName, StringComparison.InvariantCultureIgnoreCase))
                    {
                        if (String.IsNullOrEmpty(Attribute.Value)) return false;
                        Value = Attribute.Value;
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("XMLFileUtils.GetAttributeValue-String", ex);
                return false;
            }
        }
    }
}
