namespace BookShop.DataProcessor.ExportDto
{
    using System.Xml.Serialization;

    [XmlType("Book")]
    public class ExportBookDto
    {
        [XmlElement("Name")]
        public string Name { get; set; }

        [XmlElement("Date")]
        public string DateTime { get; set; }

        [XmlAttribute("Pages")]
        public int BookPage { get; set; }
    }
}
