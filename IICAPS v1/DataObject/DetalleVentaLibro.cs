namespace IICAPS_v1.DataObject
{
    public class DetalleVentaLibro
    {
        public int Id { get; set; }
        public string Libro { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio_Unitario { get; set; }
        public decimal Total { get; set; }
        public int Venta_ID { get; set; }
    }
}