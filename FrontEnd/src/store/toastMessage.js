const toastMessage = {
    namespaced: true,
    state: {
        isShowToast: false,
        contentToast: null,
    },
    mutations: {
        setIsShowToast(state, isShowToast) {
            state.isShowToast = isShowToast;
        },
        setContent(state, contentToast) {
            state.contentToast = contentToast;
        }
    },
    actions: {
        setIsShowToast({ commit }, isShow) {
            commit("setIsShowToast", isShow);
        },
        setContentToast({ commit }, contentToast) {
            commit("setContent", contentToast);
        },
    },
    getters: {
        isShowToast(state) {
            return state.isShowToast;
        },
        contentToast(state) {
            return state.contentToast;
        }
    }
}

export default toastMessage;