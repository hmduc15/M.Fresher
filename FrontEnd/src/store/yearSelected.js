const yearSelected = {
    namespaced: true,
    state: {
        yearSelected: null,
    },
    mutations: {
        setYearSelected(state, yearSelected) {
            state.yearSelected = yearSelected;
        }
    },
    actions: {
        setYearSelected({ commit }, yearSelected) {
            commit("setYearSelected", yearSelected);
        }
    },
    getters: {
        yearSelected(state) {
            return state.yearSelected;
        }
    }

}

export default yearSelected;