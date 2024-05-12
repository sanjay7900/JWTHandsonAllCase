namespace JWTHandsonAllCase.Models.MenuModel
{
    public class Menu
    {
        public int MenuItemID { get; set; }
        public string MenuItemName { get; set; }
        public string Component { get; set; }
        public int ParentID { get; set; }
    }
}
