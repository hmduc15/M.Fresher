const loading = {
     namespaced: true,
     state: {
          onLoading: false,
     },
     mutations: {
          /**
           * Mutations setLoading
           * @param {*} state 
           * @param {*} isLoading 
           */
          setLoading(state, onLoading) {
               state.isLoading = onLoading;
          }
     },
     actions: {
          /**
           * Function setLoading
           * @param {*} state 
           * @param {*} isLoading 
           */
          setLoading({ commit }, onLoading) {
               commit("setLoading", onLoading);
          }
     },
     getters: {
          /**
           * Get state isLoading
           * @param {*} state 
           * @returns 
           */
          isLoading(state) {
               return state.onLoading;
          }
     }

}

export default loading;