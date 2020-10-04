namespace IICAPS_v1.DataObject
{
    public class DetallePrestamoLibro
    {
        public int Id { get; set; }
        public string Libro_Id { get; set; }
        public Libro Libro { get; set; }
        public int Cantidad { get; set; }
        public int Prestamo_ID { get; set; }
        public bool Entregado { get; set; }
    }
}