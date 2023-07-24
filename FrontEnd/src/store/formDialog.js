const formDialog = {
    namespaced: true,
    state: {
        isShow: false,
        dataForm: null,
        formMode: null,
    },
    mutations: {
        /**
         * Mutation set state isShow
         * Author: HMDUC(29/05/2023)
         * @param {*} state 
         * @param {*} isShow 
         */
        setIsShow(state, isShow) {
            state.isShow = isShow;
        },

        /**
        * Mutation set state data form
        * Author: HMDUC(29/05/2023)
        * @param {*} state 
        * @param {*} dataForm 
        */
        setDataForm(state, dataForm) {
            state.dataForm = dataForm;
        },

        setFormMode(state, formMode) {
            state.formMode = formMode;
        },
    },
    actions: {
        /**
         * Function set show Form
         * Author: HMDUC(29/05/2023)
         * @param {*} param0 
         * @param {*} isShow 
         */
        setIsShow({ commit }, isShow) {
            commit("setIsShow", isShow);
        },

        /**
         * Function set data Form
         * Author: HMDUC(29/05/2023)
         * @param {*} param0 
         * @param {*} dataForm 
         */
        setDataForm({ commit }, dataForm) {
            commit("setDataForm", dataForm);
        },

        /**
         * Function set mode form
         * Author: HMDUC(29/05/2023)
         * @param {*} param0 
         * @param {*} formMode 
         */
        setFormMode({ commit }, formMode) {
            commit("setFormMode", formMode);
        },
    },
    getters: {
        /**
         * Get state isShow Form
         * Author: HMDUC(29/05/2023)
         */
        isShow(state) {
            return state.isShow;
        },

        /**
         * Get data form
         * Author: HMDUC(29/05/2023)
         */
        dataForm(state) {
            return state.dataForm;
        },

        /**
        * Get mode form
        * Author: HMDUC(29/05/2023)
        */
        formMode(state) {
            return state.mode;
        },
    },
};

export default formDialog;
