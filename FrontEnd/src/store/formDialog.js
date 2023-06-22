const formDialog = {
    namespaced: true,
    state: {
        isShow: false,
        dataForm: null,
        title: null,
        formMode: null,
    },
    mutations: {
        setIsShow(state, isShow) {
            state.isShow = isShow;
        },
        setDataForm(state, dataForm) {
            state.dataForm = dataForm;
        },
        setTitle(state, title) {
            state.title = title;
        },
        setFormMode(state, formMode) {
            state.formMode = formMode;
        },
    },
    actions: {
        setIsShow({ commit }, isShow) {
            commit("setIsShow", isShow);
        },
        setDataForm({ commit }, dataForm) {
            commit("setDataForm", dataForm);
        },
        setTitle({ commit }, title) {
            commit("setTitle", title);
        },
        setFormMode({ commit }, formMode) {
            commit("setFormMode", formMode);
        },
    },
    getters: {
        isShow(state) {
            return state.isShow;
        },
        dataForm(state) {
            return state.dataForm;
        },
        title(state) {
            return state.title;
        },
        formMode(state) {
            return state.mode;
        },
    },
};

export default formDialog;
