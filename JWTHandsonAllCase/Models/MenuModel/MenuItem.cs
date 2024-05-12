namespace JWTHandsonAllCase.Models.MenuModel
{
    public class MenuItem
    {
        public int MenuItemID { get; set; }
        public string MenuItemName { get; set; }
        public string Component { get; set; }
        public int ParentID { get; set; }
        public List<MenuItem> Child { get; set; } = new List<MenuItem>();
    }
}
