using BabelEngine4.ECS.Components.Menus;
using DefaultEcs;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabelEngine4.ECS.Systems
{
    public class MenuSystem : IBabelSystem
    {
        EntitySet EntitiesSet = null;

        public void Reset()
        {
            EntitiesSet = App.world.GetEntities().With<Menu>().AsSet();
        }

        public void Update()
        {
            ReadOnlySpan<Entity> Entities = EntitiesSet.GetEntities();
            // Allocate this here, use it a bunch later
            int SelectedIDReal;

            foreach (ref readonly Entity _menu in Entities)
            {
                ref Menu menu = ref _menu.Get<Menu>();

                // Don't preallocate this because... is it worth preallocating an interface??
                IEnumerable<Entity> _menuItems = _menu.GetChildren();

                SelectedIDReal = menu.SelectedID;

                // TODO: Abstract menu controls
                if (App.input.keyboard.Held(Keys.Down))
                {
                    SelectedIDReal++;
                }
                if (App.input.keyboard.Held(Keys.Up))
                {
                    SelectedIDReal--;
                }

                if (SelectedIDReal < 0)
                {
                    SelectedIDReal = _menuItems.Count() - 1;
                }
                if (SelectedIDReal >= _menuItems.Count())
                {
                    SelectedIDReal = 0;
                }

                // Apparently it's fine to get entities by value
                // THIS ASSUMES THAT CHILDREN HAVE A MenuItem COMPONENT!
                int i = 0;
                foreach (Entity _menuItem in _menuItems)
                {
                    ref MenuItem menuItem = ref _menuItem.Get<MenuItem>();
                    bool ToChange = menuItem.State == MenuItem.MenuItemState.Unset || i == SelectedIDReal;

                    if (i == SelectedIDReal)
                    {
                        if (ToChange)
                        {
                            menuItem.OnChangeState?.Invoke(_menuItem, MenuItem.MenuItemState.Hover);
                        }

                        menuItem.Active?.Invoke();

                        if (App.input.keyboard.Pressed(Keys.Enter))
                        {
                            menuItem.Select?.Invoke();
                        }
                    }
                    else
                    {
                        if (ToChange)
                        {
                            menuItem.OnChangeState?.Invoke(_menuItem, MenuItem.MenuItemState.Blur);
                        }
                    }

                    i++;
                }

                menu.SelectedID = SelectedIDReal;
            }
        }
    }
}
