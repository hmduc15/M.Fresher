import Enum from "@/utils/enum";

const escEvent = {
    mounted: (el, binding) => {
        el.escEvent = (event) => {
            if (event.keyCode === Enum.KEY__CODE.ESC) {
                binding.value();
            }
        };
        document.addEventListener("keydown", el.escEvent);
    },
    unmounted: el => {
        document.removeEventListener("keydown", el.escEvent);
        delete el.escEvent;
    },
};

export default escEvent;