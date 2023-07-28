<template>
  <div
    ref="sidebar"
    :class="isCollapsed ? 'sidebar sidebar--active' : 'sidebar'"
  >
    <div class="sidebar--top">
      <div class="sidebar__logo">
        <div class="sidebar__icon icon__logo"></div>
        <p>{{ this.$_MISAResources.name.nameSotfware }}</p>
      </div>
      <div
        :class="[
          'sidenav__menu',
          { 'sidenav__menu--scroll': isShowSub && isCollapsed },
        ]"
      >
        <router-link
          v-for="(btn, index) in buttons"
          :key="index"
          :to="btn.path"
          class="underline"
        >
          <m-button
            :iconButton="btn.icon"
            :className="[
              btn.className,
              { 'btn--active': activeIndex === index },
            ]"
            v-bind:content="btn.content"
            @click="setActive(btn, index)"
            iconSeccond="icon__arrow--down"
          >
          </m-button>
          <template v-if="isCollapsed">
            <div class="sidenav__collapse" v-if="btn.child && isShowSub">
              <router-link
                class="underline"
                v-for="(child, index) in btn.child"
                :to="child.path"
                :key="index"
                extract
              >
                <div
                  :class="[
                    'sidenav__item',
                    { 'sidenav--active': sidenavIndex == index },
                  ]"
                  @click="setActiveSub(index)"
                >
                  <div class="icon__arrow--nav"></div>
                  {{ child.content }}
                </div>
              </router-link>
            </div></template
          >
        </router-link>
      </div>
    </div>
    <div class="sidebar--bottom">
      <m-button
        className="btn__collapse"
        :iconButton="`${
          isCollapsed ? 'icon__collapse--left' : 'icon__collapse--right'
        }`"
        @click="setCollapse"
        :title="`${
          isCollapsed
            ? this.$_MISAResources.tooltip__btn.collapsed
            : this.$_MISAResources.tooltip__btn.expanded
        }`"
      ></m-button>
    </div>
  </div>
</template>
<script>
import { mapActions } from "vuex";

import MButton from "@/components/base/MButton.vue";

export default {
  name: "TheSidebar",
  components: {
    "m-button": MButton,
  },
  data() {
    return {
      isCollapsed: true,
      activeIndex: 1,
      buttons: [
        {
          icon: "icon__home",
          className: "btn__sidebar",
          content: this.$_MISAResources.sidebar.dashboard,
          path: "/",
        },
        {
          icon: "icon__poperty",
          className: "btn__sidebar",
          content: this.$_MISAResources.sidebar.asset,
          path: "/asset",
          child: [
            {
              content: this.$_MISAResources.sidebar.assetIncrease,
              path: "/assetincrease",
            },
            {
              content: this.$_MISAResources.sidebar.changeInfor,
              path: "/changeinfor",
            },
            {
              content: this.$_MISAResources.sidebar.assetAssessment,
              path: "/assetassessment",
            },
            {
              content: this.$_MISAResources.sidebar.tranfer,
              path: "/assettranfer",
            },
            {
              content: this.$_MISAResources.sidebar.receiptRecommend,
              path: "/receiptreccommend",
            },
            {
              content: this.$_MISAResources.sidebar.assetReducing,
              path: "/assetreducing",
            },
            {
              content: this.$_MISAResources.sidebar.depreciationBusiness,
              path: "/depreciationbusiness",
            },
            {
              content: this.$_MISAResources.sidebar.depreciationAsset,
              path: "/depreciationasset",
            },
            {
              content: this.$_MISAResources.sidebar.assetInventory,
              path: "/assetinventory",
            },
          ],
        },
        {
          icon: "icon__line",
          className: "btn__sidebar",
          content: this.$_MISAResources.sidebar.assetTrilist,
          path: "/tai-san-ht",
        },
        {
          icon: "icon__paint",
          className: "btn__sidebar",
          content: this.$_MISAResources.sidebar.equipmentList,
          path: "/",
        },
        {
          icon: "icon__five",
          className: "btn__sidebar",
          content: this.$_MISAResources.sidebar.dictionary,
          path: "/",
        },
        {
          icon: "icon__search--xl",
          className: "btn__sidebar",
          content: this.$_MISAResources.sidebar.search,
          path: "/",
        },
        {
          icon: "icon__report",
          className: "btn__sidebar",
          content: this.$_MISAResources.sidebar.report,
          path: "/",
        },
      ],
      isShowSub: false,
      sidenavIndex: null,
    };
  },
  methods: {
    ...mapActions("sideBar", ["setIsCollapsed"]),

    /**
     * Function set active Button Sidebar
     * @param {*} btn
     * @param {*} index
     * Author: HMDUC (20/07/2023)
     */
    setActive(btn, index) {
      this.activeIndex = index;
      if (this.buttons[index].child) {
        this.isShowSub = !this.isShowSub;
      } else {
        this.isShowSub = false;
      }
      this.sidenavIndex = -1;
    },

    /**
     * Function set active sub menu
     * Author: HMDUC (19/07/2023)
     */
    setActiveSub(index) {
      this.sidenavIndex = index;
    },

    /**
     * Function set collapse sidebar
     * Author: HMDUC (20/07/2023)
     */
    setCollapse() {
      this.isCollapsed = !this.isCollapsed;
      if (!this.isCollapsed) {
        this.isShowSub = false;
      }
      this.setIsCollapsed(this.isCollapsed);
    },
  },
};
</script>
<style scoped>
@import "@/css/base/sidebar.css";
@import "@/css/base/icon.css";
@import "@/css/base/button.css";

.sidebar__logo {
  margin-bottom: 20px;
}
</style>
