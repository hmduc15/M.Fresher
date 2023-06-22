const sideBar = {
     namespaced: true,
     state: {
          isCollapsed: false,
     },
     mutations: {
          setIsCollapsed(state, isCollapsed) {
               state.isCollapsed = isCollapsed;
          }
     },
     actions: {
          setIsCollapsed({ commit }, isCollapsed) {
               commit("setIsCollapsed", isCollapsed);
          }
     },
     getters: {
          isCollapsed(state) {
               return state.isCollapsed;
          }
     }
}

export default sideBar;