const contextMenu = {
    namespaced: true,
    state: {
        isShowMenu: false,
        pos: { x: 0, y: 0 },
        dataCMenu: null,
    },
    mutations: {
        setShowMenu(state, isShow) {
            state.isShowMenu = isShow;
        },
        setDataMenu(state, data) {
            state.dataCMenu = data;
        },
        setPos(state, pos) {
            state.pos.x = pos.x;
            state.pos.y = pos.y;
        },
    },
    actions: {
        setShowMenu({ commit }, isShowMenu) {
            commit("setShowMenu", isShowMenu);
        },
        setDataMenu({ commit }, dataCMenu) {
            commit("setDataMenu", dataCMenu);
        },
        setPos({ commit }, pos) {
            commit("setPos", pos);
        },
    },
    getters: {
        isShowMenu(state) {
            return state.isShowMenu;
        },
        dataCMenu(state) {
            return state.data;
        },
        pos(state) {
            return state.pos;
        },
    },
};

export default contextMenu;
