export default {
    //key code keyboard
    KEY__CODE: {
        //Key esc
        ESC: 27.0,
        //Key arrow up
        ARROW_UP: 38,
        //Key arrow down
        ARROW_DOWN: 40,
        //Key Ctrl
        CTRL: 17,
        //Key Shift
        SHIFT: 16,
    },
    //format date
    FORMAT__DATE: {
        // type VI
        VI: "dd/MM/yyyy",
        // type EN
        EN: "MM/dd/yyyy"
    },
    //form mode
    FORM__MODE: {
        // form add
        ADD: "add",
        // form edit
        EDIT: "edit",
    },
    //Status code response
    REQ_CODE: {
        //Success
        SUCCESS: 200,
        // Add success  
        CREATED: 201,
        // Add success  
        NO_CONTENT: 204,
        // Error Validate
        BAD_REQUEST: 400,
        // Error unknow
        ERROR: 500,

    }
}
