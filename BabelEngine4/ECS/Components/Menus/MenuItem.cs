using DefaultEcs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabelEngine4.ECS.Components.Menus
{
    public struct MenuItem
    {
        public enum MenuItemState
        {
            Unset,
            Blur,
            Hover,
        }

        public Action
            Active,
            Select
        ;

        public Action<Entity, MenuItemState> OnChangeState;

        public MenuItemState State;
    }
}
