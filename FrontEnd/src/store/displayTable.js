const displayTable = {
     namespaced: true,
     state: {
          listDisplayed: [],
     },
     mutations: {
          /**
           * Function set state listDisplayed
           * @param {*} state 
           * @param {*} listDisplayed 
           * Author: HMDUC (18/7/2023)
           */
          setListDisplayed(state, listDisplayed) {
               state.listDisplayed = listDisplayed
          }
     },
     actions: {
          /**
           * Function set listDisplayed
           * @param {*} param0 
           * @param {*} listDisplayed 
           * Author: HMDUC (18/7/2023)
           */
          setListDisplayed({ commit }, listDisplayed) {
               commit("setListDisplayed", listDisplayed)
          }
     },
     getters: {
          /**
           * Get state listDisplayed 
           * Author: HMDUC (18/7/2023)
           */
          listDisplayed(state) {
               return state.listDisplayed
          }
     }

}

export default displayTable;