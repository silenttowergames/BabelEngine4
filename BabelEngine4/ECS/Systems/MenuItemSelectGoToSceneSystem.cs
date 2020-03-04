using BabelEngine4.ECS.Components.Menus;
using DefaultEcs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabelEngine4.ECS.Systems
{
    public class MenuItemSelectGoToSceneSystem : IBabelSystem
    {
        EntitySet entities;

        public void Reset()
        {
            entities = App.world.GetEntities().With<MenuItem>().With<MenuItemSelectGoToScene>().AsSet();
        }

        public void OnLoad()
        {
            // nothing
        }

        public void Update()
        {
            foreach (ref readonly Entity entity in entities.GetEntities())
            {
                ref readonly MenuItem menuItem = ref entity.Get<MenuItem>();
                ref readonly MenuItemSelectGoToScene menuItemSelect = ref entity.Get<MenuItemSelectGoToScene>();

                if (!menuItem.Selected)
                {
                    continue;
                }

                App.Scene = App.Scenes[menuItemSelect.SceneID];

                break;
            }
        }
    }
}
