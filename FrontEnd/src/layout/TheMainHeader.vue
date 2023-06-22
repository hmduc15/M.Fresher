<template>
  <div class="main__header">
    <p class="main__header--title header--left">Danh sách tài sản</p>
    <div class="header--right">
      <p class="name__company">Sở tài chính</p>
      <div class="select__year">
        <p class="select__text--static">Năm</p>
        <m-combobox
          v-model="yearSelected"
          :listOptions="listYearOpt"
          placeholder=""
          @change="changeYear"
        ></m-combobox>
        <div class="select__icon">
          <m-button iconButton="icon__up" @click="prevYear"></m-button>
          <m-button iconButton="icon__down " @click="nextYear"></m-button>
        </div>
      </div>
      <m-button
        type="button"
        className="header__icon"
        iconButton="icon__notice"
        isEffect="dark"
        :title="this.$_MISAResources.tooltip__btn.notice"
      ></m-button>
      <m-button
        className="header__icon"
        iconButton="icon__menu"
        isEffect="dark"
        :title="this.$_MISAResources.tooltip__btn.setting"
      ></m-button>
      <m-button
        className="header__icon"
        iconButton="icon__ques"
        isEffect="dark"
        :title="this.$_MISAResources.tooltip__btn.helper"
      ></m-button>
      <div class="header__icon">
        <m-button
          iconButton="icon__avatar"
          isEffect="dark"
          :title="this.$_MISAResources.tooltip__btn.profile"
        ></m-button>
        <m-button
          iconButton="icon__down"
          isEffect="dark"
          :title="this.$_MISAResources.tooltip__btn.profile"
        ></m-button>
      </div>
    </div>
  </div>
</template>
<script>
import MCombobox from "@/components/base/MCombobox.vue";
import MButton from "@/components/base/MButton.vue";

import { mapActions } from "vuex";

export default {
  name: "TheMainHeader",
  components: {
    "m-combobox": MCombobox,
    "m-button": MButton,
  },
  data() {
    return {
      listYearOpt: [
        {
          value: 2021,
          label: 2021,
        },
        {
          value: 2022,
          label: 2022,
        },
        {
          value: 2023,
          label: 2023,
        },
      ],
      indexYear: 1,
      yearSelected: 2023,
    };
  },
  watch: {
    yearSelected(newYear) {
      this.setYearSelected(newYear);
    },
  },

  mounted() {
    this.setYearSelected(this.yearSelected);
  },
  methods: {
    ...mapActions("yearSelected", ["setYearSelected"]),
    /**
     * function change year binding UI
     * Author: HMDUC (28/05/2023)
     * @param {} value
     */
    changeYear(value) {
      this.yearSelected = value;
    },

    /**
     * Function handle btn next yeat
     * Author: HMDUC (28/05/2023)
     */
    nextYear() {
      if (this.indexYear < this.listYearOpt.length - 1) {
        this.indexYear++;
        this.yearSelected = this.listYearOpt[this.indexYear].label;
      }
    },

    /**
     * Function handle btn prev yeat
     * Author: HMDUC (28/05/2023)
     */
    prevYear() {
      if (this.indexYear > 0) {
        this.indexYear--;
        this.yearSelected = this.listYearOpt[this.indexYear].label;
      }
    },
  },
};
</script>
<style>
@import "@/css/base/header.css";
@import "@/css/base/icon.css";
</style>
