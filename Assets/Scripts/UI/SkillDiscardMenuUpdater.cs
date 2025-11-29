using UI.BattleMenu;
using UI.Menus;
using UI.Menus.Data;

namespace UI
{
    public class SkillDiscardMenuUpdater : MenuItemsUpdater
    {
        private bool _newSkillSelected;
        private int _prevSelectedIndex;
        public MenuItemData NewSkillData { get; private set; }

        public void SetNewSkill(MenuItemData newSkillData)
        {
            NewSkillData = newSkillData;
            _newSkillSelected = false;
            SelectedIndex = 0;
            _prevSelectedIndex = SelectedIndex;
        }

        public override void ExecuteSelection()
        {
            if (_newSkillSelected)
            {
                NewSkillData.OnSelected?.Invoke();
                return;
            }

            MenuItems[SelectedIndex].OnSelected?.Invoke();
        }

        public void ProcessInput(int verticalInput, int horizontalInput)
        {
            if (verticalInput != 0)
            {
                ProcessVerticalInput(verticalInput);
                return;
            }

            ProcessHorizontalInput();
        }

        private void ProcessHorizontalInput()
        {
            if (_newSkillSelected)
            {
                MoveToOldSkills();
                return;
            }

            MoveToNewSkill();
        }

        private void ProcessVerticalInput(int verticalInput)
        {
            if (_newSkillSelected)
                return;

            UpdateSelection(verticalInput);
        }

        private void MoveToOldSkills()
        {
            SelectedIndex = _prevSelectedIndex;
            _newSkillSelected = false;
            NewSkillData.IsHighlighted.Value = false;
            ResetSelection();
        }

        private void MoveToNewSkill()
        {
            _prevSelectedIndex = SelectedIndex;
            _newSkillSelected = true;

            SelectedIndex = -1;
            ApplyHighlight();
            NewSkillData.IsHighlighted.Value = true;
        }
    }
}