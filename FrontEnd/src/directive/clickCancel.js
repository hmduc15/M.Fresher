import Enum from "@/utils/enum";

const ctrlCancel = {
     mounted: (el, binding) => {
          el.ctrlCancelEvent = event => {
               if (event.ctrlKey && event.keyCode === Enum.KEY__CODE.Q) {
                    event.preventDefault();
                    binding.value();
               }
          }
          document.addEventListener("keydown", el.ctrlCancelEvent);
     },
     unmounted: el => {
          document.removeEventListener("keydown", el.ctrlCancelEvent);
          delete el.ctrlCancelEvent;

     }
}

export default ctrlCancel;