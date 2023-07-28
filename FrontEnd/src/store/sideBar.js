const sideBar = {
     namespaced: true,
     state: {
          isCollapsed: true,
     },
     mutations: {
          /**
           * Mutations set state isCollapsed
           * Author: HMDUC(29/05/2023)
           * @param {*} state 
           * @param {*} isCollapsed 
           */
          setIsCollapsed(state, isCollapsed) {
               state.isCollapsed = isCollapsed;
          }
     },
     actions: {
          /**
           * Function set isCollapsed sidebar
           * Author: HMDUC(29/05/2023)
           * @param {*} param0 
           * @param {*} isCollapsed 
           */
          setIsCollapsed({ commit }, isCollapsed) {
               commit("setIsCollapsed", isCollapsed);
          }
     },
     getters: {
          /**
           * Get state isCollapsed
           * Author: HMDUC(29/05/2023) 
           */
          isCollapsed(state) {
               return state.isCollapsed;
          }
     }
}

export default sideBar;