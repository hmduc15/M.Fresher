const inputError = {
     namespaced: true,
     state: {
          listError: [],
     },
     mutations: {
          /**
           * Mutation set set state listError
           * @param {*} state 
           * @param {*} listError 
           * Author: HMDUC (16/07/2023)
           */
          setListError(state, listError) {
               state.listError = listError
          }
     },
     actions: {
          /**
           * Function set listError
           * @param {*} param0 
           * @param {*} listError 
           * Author: HMDUC (16/07/2023)
           */
          setListError({ commit }, listError) {
               commit("setListError", listError);
          }
     },
     getters: {
          /**
           * Get state listError
           * @param {*} state 
           * Author: HMDUC (16/07/2023)
           */
          listError(state) {
               return state.listError;
          }
     }

}

export default inputError;