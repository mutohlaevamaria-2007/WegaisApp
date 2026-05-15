using System;
using System.Xml.Serialization;

namespace WegaisApp.Core.XMLIntegration
{
    // МОДЕЛЬ ДЛЯ СЕРИАЛИЗАЦИИ XML ДОКУМЕНТОВ ReplyRests и ReplyRestsShop

    [Serializable()]
    [XmlType(AnonymousType = true, Namespace = "http://fsrar.ru/WEGAIS/WB_DOC_SINGLE_01")]
    [XmlRoot(Namespace = "http://fsrar.ru/WEGAIS/WB_DOC_SINGLE_01", IsNullable = false)]
    public partial class Documents
    {
        public DocumentsOwner Owner { get; set; } = null!;
        public DocumentsDocument Document { get; set; } = null!;
    }

    [Serializable()]
    [XmlType(AnonymousType = true, Namespace = "http://fsrar.ru/WEGAIS/WB_DOC_SINGLE_01")]
    public partial class DocumentsOwner
    {
        public uint FSRAR_ID { get; set; }
    }

    [Serializable()]
    [XmlType(AnonymousType = true, Namespace = "http://fsrar.ru/WEGAIS/WB_DOC_SINGLE_01")]
    public partial class DocumentsDocument
    {
        public DocumentsDocumentReplyRests_v2 ReplyRests_v2 { get; set; } = null!;
        public DocumentsDocumentReplyRestsShop_v2 ReplyRestsShop_v2 { get; set; } = null!;
    }
    // ==================== ОТВЕТ ====================
    public interface IReplyRests
    {
        DateTime RestsDate { get; }
        IStockPosition[] Products { get; }
    }
    // ---------- ReplyRests ----------
    [Serializable()]
    [XmlType(AnonymousType = true, Namespace = "http://fsrar.ru/WEGAIS/WB_DOC_SINGLE_01")]
    public partial class DocumentsDocumentReplyRests_v2 : IReplyRests
    {
        [XmlElement(Namespace = "http://fsrar.ru/WEGAIS/ReplyRests_v2")]
        public DateTime RestsDate { get; set; }

        [XmlArray(Namespace = "http://fsrar.ru/WEGAIS/ReplyRests_v2")]
        [XmlArrayItem("StockPosition", IsNullable = false)]
        public ProductsStockPosition[] Products { get; set; } = null!;

        IStockPosition[] IReplyRests.Products => Products;
    }
    // ---------- ReplyRests Shop ----------
    [Serializable()]
    [XmlType(AnonymousType = true, Namespace = "http://fsrar.ru/WEGAIS/WB_DOC_SINGLE_01")]
    public partial class DocumentsDocumentReplyRestsShop_v2 : IReplyRests
    {
        [XmlElement(Namespace = "http://fsrar.ru/WEGAIS/ReplyRestsShop_v2")]
        public DateTime RestsDate { get; set; }

        [XmlArray(Namespace = "http://fsrar.ru/WEGAIS/ReplyRestsShop_v2")]
        [XmlArrayItem("ShopPosition", IsNullable = false)]
        public ProductsShopPosition[] Products { get; set; } = null!;

        IStockPosition[] IReplyRests.Products => Products;
    }
    // ==================== ОСТАТОК ====================
    public interface IStockPosition
    {
        decimal Quantity { get; }
        IProduct Product { get; }
    }
    // ---------- ReplyRests ----------
    [Serializable()]
    [XmlType(AnonymousType = true, Namespace = "http://fsrar.ru/WEGAIS/ReplyRests_v2")]
    public partial class ProductsStockPosition : IStockPosition
    {
        public decimal Quantity { get; set; }
        public string InformF1RegId { get; set; } = null!;
        public string InformF2RegId { get; set; } = null!;
        public ProductsStockPositionProduct Product { get; set; } = null!;
        IProduct IStockPosition.Product => Product;
    }
    // ---------- ReplyRests Shop ----------
    [Serializable()]
    [XmlType(AnonymousType = true, Namespace = "http://fsrar.ru/WEGAIS/ReplyRestsShop_v2")]
    public partial class ProductsShopPosition : IStockPosition
    {
        public decimal Quantity { get; set; }
        public ProductsShopPositionProduct Product { get; set; } = null!;
        IProduct IStockPosition.Product => Product;
    }
    // ==================== ПРОДУКТ ====================
    public interface IProduct
    {
        string FullName { get; }
        ulong AlcCode { get; }
        decimal Capacity { get; }
        string UnitType { get; }
        decimal AlcVolume { get; }
        ushort ProductVCode { get; }
        Producer Producer { get; }
    }
    // ---------- ReplyRests ----------
    [Serializable()]
    [XmlType(AnonymousType = true, Namespace = "http://fsrar.ru/WEGAIS/ReplyRests_v2")]
    public partial class ProductsStockPositionProduct : IProduct
    {
        [XmlElement(Namespace = "http://fsrar.ru/WEGAIS/ProductRef_v2")]
        public string FullName { get; set; } = null!;
        [XmlElement(Namespace = "http://fsrar.ru/WEGAIS/ProductRef_v2")]
        public ulong AlcCode { get; set; }
        [XmlElement(Namespace = "http://fsrar.ru/WEGAIS/ProductRef_v2")]
        public decimal Capacity { get; set; }
        [XmlElement(Namespace = "http://fsrar.ru/WEGAIS/ProductRef_v2")]
        public string UnitType { get; set; } = null!;
        [XmlElement(Namespace = "http://fsrar.ru/WEGAIS/ProductRef_v2")]
        public decimal AlcVolume { get; set; }
        [XmlElement(Namespace = "http://fsrar.ru/WEGAIS/ProductRef_v2")]
        public ushort ProductVCode { get; set; }
        [XmlElement(Namespace = "http://fsrar.ru/WEGAIS/ProductRef_v2")]
        public Producer Producer { get; set; } = null!;
    }
    // ---------- ReplyRests Shop ----------
    [Serializable()]
    [XmlType(AnonymousType = true, Namespace = "http://fsrar.ru/WEGAIS/ReplyRestsShop_v2")]
    public partial class ProductsShopPositionProduct : IProduct
    {
        [XmlElement(Namespace = "http://fsrar.ru/WEGAIS/ProductRef_v2")]
        public string FullName { get; set; } = null!;
        [XmlElement(Namespace = "http://fsrar.ru/WEGAIS/ProductRef_v2")]
        public ulong AlcCode { get; set; }
        [XmlElement(Namespace = "http://fsrar.ru/WEGAIS/ProductRef_v2")]
        public decimal Capacity { get; set; }
        [XmlElement(Namespace = "http://fsrar.ru/WEGAIS/ProductRef_v2")]
        public string UnitType { get; set; } = null!;
        [XmlElement(Namespace = "http://fsrar.ru/WEGAIS/ProductRef_v2")]
        public decimal AlcVolume { get; set; }
        [XmlElement(Namespace = "http://fsrar.ru/WEGAIS/ProductRef_v2")]
        public ushort ProductVCode { get; set; }
        [XmlElement(Namespace = "http://fsrar.ru/WEGAIS/ProductRef_v2")]
        public Producer Producer { get; set; } = null!;
    }
    // ==================== ПРОИЗВОДИТЕЛЬ ====================
    [Serializable()]
    [XmlType(AnonymousType = true, Namespace = "http://fsrar.ru/WEGAIS/ProductRef_v2")]
    [XmlRoot(Namespace = "http://fsrar.ru/WEGAIS/ProductRef_v2", IsNullable = false)]
    public partial class Producer
    {
        [XmlElement(Namespace = "http://fsrar.ru/WEGAIS/ClientRef_v2")]
        public TS TS { get; set; } = null!;
        [XmlElement(Namespace = "http://fsrar.ru/WEGAIS/ClientRef_v2")]
        public FO FO { get; set; } = null!;
        [XmlElement(Namespace = "http://fsrar.ru/WEGAIS/ClientRef_v2")]
        public UL UL { get; set; } = null!;
    }

    // ---------- ВИДЫ ПРОИЗВОДИТЕЛЕЙ, АДРЕСА ----------
    [Serializable()]
    [XmlType(AnonymousType = true, Namespace = "http://fsrar.ru/WEGAIS/ClientRef_v2")]
    [XmlRoot(Namespace = "http://fsrar.ru/WEGAIS/ClientRef_v2", IsNullable = false)]
    public partial class TS
    {
        public ulong ClientRegId { get; set; }
        public string FullName { get; set; } = null!;
        public string ShortName { get; set; } = null!;
        public TSAddress address { get; set; } = null!;
    }

    [Serializable()]
    [XmlType(AnonymousType = true, Namespace = "http://fsrar.ru/WEGAIS/ClientRef_v2")]
    public partial class TSAddress
    {
        public byte Country { get; set; }
        public string description { get; set; } = null!;
    }

    [Serializable()]
    [XmlType(AnonymousType = true, Namespace = "http://fsrar.ru/WEGAIS/ClientRef_v2")]
    [XmlRoot(Namespace = "http://fsrar.ru/WEGAIS/ClientRef_v2", IsNullable = false)]
    public partial class FO
    {
        public ulong ClientRegId { get; set; }
        public string FullName { get; set; } = null!;
        public string ShortName { get; set; } = null!;
        public FOAddress address { get; set; } = null!;
    }

    [Serializable()]
    [XmlType(AnonymousType = true, Namespace = "http://fsrar.ru/WEGAIS/ClientRef_v2")]
    public partial class FOAddress
    {
        public ushort Country { get; set; }
        public string description { get; set; } = null!;
    }

    [Serializable()]
    [XmlType(AnonymousType = true, Namespace = "http://fsrar.ru/WEGAIS/ClientRef_v2")]
    [XmlRoot(Namespace = "http://fsrar.ru/WEGAIS/ClientRef_v2", IsNullable = false)]
    public partial class UL
    {
        public ulong ClientRegId { get; set; }
        public ulong INN { get; set; }
        public uint KPP { get; set; }
        public string FullName { get; set; } = null!;
        public string ShortName { get; set; } = null!;
        public ULAddress address { get; set; } = null!;
    }

    [Serializable()]
    [XmlType(AnonymousType = true, Namespace = "http://fsrar.ru/WEGAIS/ClientRef_v2")]
    public partial class ULAddress
    {
        public ushort Country { get; set; }
        public byte RegionCode { get; set; }
        public string description { get; set; } = null!;
    }
}