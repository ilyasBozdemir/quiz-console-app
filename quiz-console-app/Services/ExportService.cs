using quiz_console_app.ViewModels;
using System.Xml;

namespace quiz_console_app.Services;

public class ExportService
{
    public void ExportToXml(List<BookletViewModel> booklets, string xmlFilePath)
    {
        XmlDocument xmlDoc = CreateXmlDocument(booklets);

        xmlDoc.Save(xmlFilePath);
    }

    public void ExportAnswerKeyToXsl(string xslFilePath)
    {
        string xslContent = @"<?xml version=""1.0"" encoding=""UTF-8""?>
    <xsl:stylesheet version=""1.0"" xmlns:xsl=""http://www.w3.org/1999/XSL/Transform"">
        <xsl:template match=""/"">
            <html>
                <head>
                    <title>Answer Key Report</title>
                </head>
                <body>
                    <h1>Answer Key Report</h1>
                    <xsl:apply-templates select=""AnswerKeys""/>
                </body>
            </html>
        </xsl:template>

        <xsl:template match=""AnswerKeys"">
            <xsl:for-each select=""AnswerKey"">
                <div>
                    <h2><xsl:value-of select=""BookletId""/></h2>
                    <p>Soru Id: <xsl:value-of select=""QuestionId""/></p>
                    <p>Doğru Seçenek Id: <xsl:value-of select=""CorrectOptionId""/></p>
                </div>
            </xsl:for-each>
        </xsl:template>
    </xsl:stylesheet>";

        File.WriteAllText(xslFilePath, xslContent);
    }

    public void ExportAnswerKeyToXsd(string xsdFilePath)
    {
        string xsdContent = @"<?xml version=""1.0"" encoding=""UTF-8""?>
    <xs:schema xmlns:xs=""http://www.w3.org/2001/XMLSchema"">
        <xs:element name=""AnswerKeys"">
            <xs:complexType>
                <xs:sequence>
                    <xs:element name=""AnswerKey"" maxOccurs=""unbounded"">
                        <xs:complexType>
                            <xs:sequence>
                                <xs:element name=""BookletId"" type=""xs:int""/>
                                <xs:element name=""QuestionId"" type=""xs:int""/>
                                <xs:element name=""CorrectOptionId"" type=""xs:int""/>
                            </xs:sequence>
                        </xs:complexType>
                    </xs:element>
                </xs:sequence>
            </xs:complexType>
        </xs:element>
    </xs:schema>";

        File.WriteAllText(xsdFilePath, xsdContent);
    }


    public void ExportToXsl(string xslFilePath)
    {
        string xslContent = @"<?xml version=""1.0"" encoding=""UTF-8""?>
    <xsl:stylesheet version=""1.0"" xmlns:xsl=""http://www.w3.org/1999/XSL/Transform"">
        <xsl:template match=""/"">
            <html>
                <head>
                    <title>Booklets Report</title>
                </head>
                <body>
                    <h1>Booklets Report</h1>
                    <xsl:apply-templates select=""Booklets""/>
                </body>
            </html>
        </xsl:template>

        <xsl:template match=""Booklets"">
            <xsl:for-each select=""Booklet"">
                <div>
                    <h2><xsl:value-of select=""BookletName""/></h2>
                    <!-- Diğer kitapçık bilgileri burada eklenebilir -->
                </div>
            </xsl:for-each>
        </xsl:template>
    </xsl:stylesheet>";

        File.WriteAllText(xslFilePath, xslContent);
    }

    public void ExportToXsd(string xsdFilePath)
    {
        string xsdContent = @"<?xml version=""1.0"" encoding=""UTF-8""?>
    <xs:schema xmlns:xs=""http://www.w3.org/2001/XMLSchema"">
        <xs:element name=""Booklets"">
            <xs:complexType>
                <xs:sequence>
                    <xs:element name=""Booklet"" maxOccurs=""unbounded"">
                        <xs:complexType>
                            <xs:sequence>
                                <!-- Kitapçık veri alanları burada tanımlanabilir -->
                                <xs:element name=""BookletName"" type=""xs:string""/>
                            </xs:sequence>
                        </xs:complexType>
                    </xs:element>
                </xs:sequence>
            </xs:complexType>
        </xs:element>
    </xs:schema>";

        File.WriteAllText(xsdFilePath, xsdContent);
    }


    private XmlDocument CreateXmlDocument(List<BookletViewModel> booklets)
    {
        XmlDocument xmlDoc = new XmlDocument();

        XmlElement rootElement = xmlDoc.CreateElement("Booklets");
        xmlDoc.AppendChild(rootElement);

        // Her bir kitapçık için XML elementi oluştur ve kök elemente ekle
        foreach (var booklet in booklets)
        {
            XmlElement bookletElement = xmlDoc.CreateElement("Booklet");
            // Kitapçık verilerini XML elementine ekleme işlemi burada yapılabilir
            rootElement.AppendChild(bookletElement);
        }

        return xmlDoc;
    }
}
