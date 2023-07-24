import Enum from "@/utils/enum";

const moveRow = {
     mounted: (el, binding) => {
          el.handleKeyDown = (event) => {
               const isDatePickerActive = document.querySelector(".dp__outer_menu_wrap");
               if (!isDatePickerActive) {
                    if (event.keyCode == Enum.KEY__CODE.ARROW_UP) {
                         binding.value.moveRow(-1)
                    } else if (event.keyCode === Enum.KEY__CODE.ARROW_DOWN) {
                         binding.value.moveRow(1);
                    }
               }
          }
          document.addEventListener("keydown", el.handleKeyDown);
     },
     unmounted: el => {
          document.removeEventListener("keydown", el.handleKeyDown);
          delete el.handleKeyDown;
     }
}

export default moveRow;