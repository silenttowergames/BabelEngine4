using BabelEngine4.Assets.Tiled;
using BabelEngine4.ECS.Components;
using BabelEngine4.ECS.Components.Menus;
using BabelEngine4.ECS.Components.Rendering;
using DefaultEcs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabelEngine4.ECS.Entities
{
    public class TestMenuFactory : IEntityFactory
    {
        static string
            menuItemHelloStr = "Hello!",
            menuItemGoodbyeStr = "Goodbye.",
            menuItemMiddleStr = "Middle"
        ;

        public Entity Create(float LayerDepth, int LayerID, float Parallax, List<TiledProperty> properties = null)
        {
            Entity menu = App.world.CreateEntity();
            menu.Set(new Menu() { HoverColor = Color.Blue, BlurColor = Color.Yellow, Name = "menu0" });

            Entity menuItemHello = App.world.CreateEntity();
            menuItemHello.Set(new Body());
            menuItemHello.Set(new Text(menuItemHelloStr) { color = new Color(40, 30, 255), spriteFont = "PressStart2P", LayerDepth = LayerDepth, LayerID = LayerID, Parallax = Parallax });
            menuItemHello.Set(new MenuItem());
            menuItemHello.Set(new MenuItemSelectGoToScene() { SceneID = "testscene0" });
            menu.SetAsParentOf(menuItemHello);
            
            Entity menuItemMiddle = App.world.CreateEntity();
            menuItemMiddle.Set(new Body() { Position = new Vector2(0, 10) });
            menuItemMiddle.Set(new Text(menuItemMiddleStr) { color = new Color(40, 30, 255), spriteFont = "PressStart2P", LayerDepth = LayerDepth, LayerID = LayerID, Parallax = Parallax });
            menuItemMiddle.Set(new MenuItem());
            menu.SetAsParentOf(menuItemMiddle);

            Entity menuItemGoodbye = App.world.CreateEntity();
            menuItemGoodbye.Set(new Body() { Position = new Vector2(0, 20) });
            menuItemGoodbye.Set(new Text(menuItemGoodbyeStr) { color = new Color(40, 30, 255), spriteFont = "PressStart2P", LayerDepth = LayerDepth, LayerID = LayerID, Parallax = Parallax });
            menuItemGoodbye.Set(new MenuItem());
            menuItemGoodbye.Set(new MenuItemSelectGoToScene() { SceneID = "mapscene" });
            menu.SetAsParentOf(menuItemGoodbye);

            return menu;
        }
    }
}
