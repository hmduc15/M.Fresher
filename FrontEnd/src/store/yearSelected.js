const yearSelected = {
    namespaced: true,
    state: {
        yearSelected: null,
    },
    mutations: {
        /**
        * Mutations set state isCollapsed
        * Author: HMDUC(29/05/2023)
        * @param {*} state 
        * @param {*} yearSelected 
        */
        setYearSelected(state, yearSelected) {
            state.yearSelected = yearSelected;
        }
    },
    actions: {
        /**
         * Function set year 
         * Author: HMDUC(29/05/2023)
         * @param {*} param0 
         * @param {*} yearSelected 
         */
        setYearSelected({ commit }, yearSelected) {
            commit("setYearSelected", yearSelected);
        }
    },
    getters: {
        /**
         * Get year selected
         * Author: HMDUC(29/05/2023)
         */
        yearSelected(state) {
            return state.yearSelected;
        }
    }

}

export default yearSelected;