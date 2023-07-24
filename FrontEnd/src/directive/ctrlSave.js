import Enum from "@/utils/enum";

const ctrlSave = {
     mounted: (el, binding) => {
          el.ctrlSaveEvent = event => {
               if (event.ctrlKey && event.keyCode === Enum.KEY__CODE.S) {
                    event.preventDefault();
                    binding.value();
               }
          }
          document.addEventListener("keydown", el.ctrlSaveEvent);
     },
     unmounted: el => {
          document.removeEventListener("keydown", el.ctrlSaveEvent);
          delete el.ctrlSaveEvent;

     }
}

export default ctrlSave;