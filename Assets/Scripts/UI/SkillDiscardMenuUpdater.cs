using StateMachine.BattleMenu;

namespace UI
{
    public class SkillDiscardMenuUpdater : MenuItemsUpdater
    {
        private bool _newSkillSelected = false;
        private int _prevSelectedIndex = 0;

        private MenuItemData _newSkillData;
        public MenuItemData NewSkillData => _newSkillData;
        
        public void SetNewSkill(MenuItemData newSkillData)
        {
            _newSkillData = newSkillData;
            _newSkillSelected = false;
            SelectedIndex = 0;
            _prevSelectedIndex = SelectedIndex;
        }

        public override void ExecuteSelection()
        {
            if (_newSkillSelected)
            {
                _newSkillData.OnSelected?.Invoke();
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
            _newSkillData.IsHighlighted.Value = false;
            ResetSelection();
        }

        private void MoveToNewSkill()
        {
            _prevSelectedIndex = SelectedIndex;
            _newSkillSelected = true;

            SelectedIndex = -1;
            ApplyHighlight();
            _newSkillData.IsHighlighted.Value = true;
        }
    }
}