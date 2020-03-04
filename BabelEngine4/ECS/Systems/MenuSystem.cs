using BabelEngine4.ECS.Components.Menus;
using BabelEngine4.ECS.Components.Rendering;
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
        EntitySet
            EntitiesSet = null,
            DisabledEntitiesSet = null
        ;

        public Func<int> ChangeSelected = null;

        public string CurrentMenu = null;

        public void Reset()
        {
            EntitiesSet = App.world.GetEntities().With<Menu>().AsSet();
            DisabledEntitiesSet = App.world.GetDisabledEntities().With<Menu>().AsSet();
        }

        public void OnLoad()
        {
            SetMenu(null, true);
        }

        public void SetMenu(string Menu, bool Force = false)
        {
            if (!Force && CurrentMenu == Menu)
            {
                return;
            }

            CurrentMenu = Menu;

            foreach(ref readonly Entity _menu in EntitiesSet.GetEntities())
            {
                _menu.Disable();

                foreach (Entity menuItem in _menu.GetChildren())
                {
                    menuItem.Disable();
                }
            }

            if (CurrentMenu != null)
            {
                foreach (ref readonly Entity _menu in DisabledEntitiesSet.GetEntities())
                {
                    ref Menu menu = ref _menu.Get<Menu>();

                    if (menu.Name != CurrentMenu)
                    {
                        continue;
                    }

                    _menu.Enable();

                    foreach (Entity menuItem in _menu.GetChildren())
                    {
                        menuItem.Enable();
                    }
                }
            }
        }

        public void Update()
        {
            if (CurrentMenu == null)
            {
                return;
            }

            ReadOnlySpan<Entity> Entities = EntitiesSet.GetEntities();
            IEnumerable<Entity> _menuItems;
            // Allocate this here, use it a bunch later
            int SelectedIDReal;

            foreach (ref readonly Entity _menu in Entities)
            {
                ref Menu menu = ref _menu.Get<Menu>();

                if (CurrentMenu != menu.Name)
                {
                    continue;
                }

                _menuItems = _menu.GetChildren();

                SelectedIDReal = menu.SelectedID;

                if (ChangeSelected != null)
                {
                    SelectedIDReal += ChangeSelected();
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
                    ref Text menuItemText = ref _menuItem.Get<Text>();

                    menuItem.Selected = false;

                    if (i == SelectedIDReal)
                    {
                        menuItem.State = MenuItem.MenuItemState.Hover;
                        menuItemText.color = menu.HoverColor;

                        if (App.input.keyboard.Pressed(Keys.Enter))
                        {
                            menuItem.Selected = true;
                        }
                    }
                    else
                    {
                        menuItem.State = MenuItem.MenuItemState.Blur;
                        menuItemText.color = menu.BlurColor;
                    }

                    i++;
                }

                menu.SelectedID = SelectedIDReal;
            }
        }
    }
}
