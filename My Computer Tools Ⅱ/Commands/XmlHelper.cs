using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace My_Computer_Tools_Ⅱ
{
    class ClsXMLoperate
    {
        //定义变量
        private static XmlDocument objXmlDoc = null;
        private static string XmlFilePath;

        public ClsXMLoperate(string _XmlFilePath)
        {
            objXmlDoc = new XmlDocument();
            XmlFilePath = _XmlFilePath;

            if (File.Exists(_XmlFilePath))
            {
                objXmlDoc.Load(_XmlFilePath);
            }
            else
            {
                throw new Exception("文件不存在");
            }
        }



        /// &lt;summary&gt;
        /// 添加一个节点及次节点的子节点
        /// &lt;/summary&gt;
        /// &lt;param name="_XmlFilePath"&gt;文件路径&lt;/param&gt;
        /// &lt;param name="_ParentNode"&gt;父节点&lt;/param&gt;
        /// &lt;param name="ChildNode"&gt;子节点名称&lt;/param&gt;
        /// &lt;param name="Content"&gt;&lt;/param&gt;
        public bool InsertSingleNode(string _ParentNode, string ChildNode, string Content="")
        {
            try
            {
                //objXmlDoc.Load(_XmlFilePath);
                //插入一节点
                XmlNode objRootNode = objXmlDoc.SelectSingleNode(_ParentNode);
                XmlElement objChildNode = objXmlDoc.CreateElement(ChildNode);
                if (Content!="")
                    objChildNode.InnerText = Content;
                objRootNode.AppendChild(objChildNode);

                objXmlDoc.Save(XmlFilePath);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        /// &lt;summary&gt;
        /// 修改一个节点内容，使用方式如下
        /// xmlTool.UpdateXmlNode("c:\filepath\xml.xml","Book/Authors[ISBN=\"0002\"]/Content","contents);
        /// &lt;/summary&gt;
        /// &lt;param name="XmlPathNode"&gt;&lt;/param&gt;
        /// &lt;param name="Content"&gt;&lt;/param&gt;
        /// &lt;returns&gt;&lt;/returns&gt;
        public bool UpdateXmlNode(string XmlPathNode, string Content)
        {
            //更新内容。
            try
            {
                objXmlDoc.SelectSingleNode(XmlPathNode).InnerText = Content;
                objXmlDoc.Save(XmlFilePath);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// &lt;summary&gt;
        /// 获取节点内容
        /// &lt;/summary&gt;
        /// &lt;param name="XmlNode"&gt;&lt;/param&gt;
        /// &lt;returns&gt;&lt;/returns&gt;
        public string GetNodeContent(string XmlNode)
        {
            return objXmlDoc.SelectSingleNode(XmlNode).InnerText;
        }

        /// <summary>
        /// 删除节点
        /// </summary>
        /// <param name="rootpath"></param>
        /// <param name="XmlPathNode"></param>
        public void Delete(string rootpath,string XmlPathNode)
        {
            var root = objXmlDoc.SelectSingleNode(rootpath);
            var element = objXmlDoc.SelectSingleNode(XmlPathNode);
            root.RemoveChild(element);


            SaveXml();
        }


        /// <summary>
        /// 检查节点是否存在
        /// </summary>
        /// <param name="rootpath"></param>
        /// <returns></returns>
        public bool CheckNode(string rootpath)
        {
            var root = objXmlDoc.SelectSingleNode(rootpath);
            if (root==null)
                return false;
            return true;
        }

        /// <summary>
        /// 取节点的字节点所有名字
        /// </summary>
        /// <returns></returns>
        public List<string> GetNodeVs()
        {
            List<string> vs = new List<string>();

            return vs;
        }

        /// <summary>
        /// 保存xml文件
        /// </summary>
        public void SaveXml()
        {
            try
            {
                objXmlDoc.Save(XmlFilePath);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }

        }
    }
}
