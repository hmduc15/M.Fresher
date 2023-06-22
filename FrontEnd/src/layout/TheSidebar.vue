<template>
  <div :class="isCollapsed ? 'sidebar sidebar--active' : 'sidebar'">
    <div class="sidebar--top">
      <div class="sidebar__logo">
        <div class="sidebar__icon icon__logo"></div>
        <p>MISA QLTS</p>
      </div>
      <router-link
        v-for="(btn, index) in buttons"
        v-bind:key="index"
        :to="btn.path || ''"
        class="underline"
      >
        <m-button
          v-bind:iconButton="btn.icon"
          v-bind:className="[
            btn.className,
            { 'btn--active': activeIndex === index },
          ]"
          v-bind:content="btn.content"
          @click="setActive(btn, index)"
        >
        </m-button>
      </router-link>
    </div>
    <div class="sidebar--bottom">
      <m-button
        className="btn__collapse"
        :iconButton="`${
          isCollapsed ? 'icon__collapse--left' : 'icon__collapse--right'
        }`"
        @click="setCollapse()"
        :title="`${isCollapsed ? 'Thu gọn' : 'Mở rộng'}`"
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
      isCollapsed: false,
      activeIndex: 1,
      buttons: [
        {
          icon: "icon__home",
          className: "btn__sidebar",
          content: "Tổng quan",
          path: "/",
        },
        {
          icon: "icon__poperty",
          className: "btn__sidebar",
          content: "Tài sản",
          path: "/tai-san",
        },
        {
          icon: "icon__line",
          className: "btn__sidebar",
          content: "Tài sản HT-ĐB",
          path: "/tai-san-ht",
        },
        {
          icon: "icon__paint",
          className: "btn__sidebar",
          content: "Công cụ dụng cụ",
          path: "/công-cụ",
        },
        {
          icon: "icon__five",
          className: "btn__sidebar",
          content: "Danh mục",
          path: "/danh mục",
        },
        {
          icon: "icon__search--xl",
          className: "btn__sidebar",
          content: "Tra cứu",
        },
        { icon: "icon__report", className: "btn__sidebar", content: "Báo cáo" },
      ],
    };
  },
  methods: {
    ...mapActions("sideBar", ["setIsCollapsed"]),

    setActive(btn, index) {
      this.activeIndex = index;
    },
    setCollapse() {
      this.isCollapsed = !this.isCollapsed;
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
