<template>
  <div class="main__content">
    <!-- Content top -->
    <div class="content__top">
      <div class="content__top--left">
        <div class="content__top--wrapper">
          <m-input
            type="text"
            name=""
            id=""
            placeHolder="Tìm kiếm tài sản"
            className="input__text input__text--icon input--default"
            :required="false"
            :isLabel="false"
            v-model="inputSearch"
            @input="clearSearch(inputSearch)"
            @keydown.enter="handleSearch(inputSearch)"
          >
          </m-input>
          <div class="icon__search--sm btn__search"></div>
        </div>
        <div class="content__top--wrapper">
          <m-combobox
            iconPrefix="icon__filter"
            iconPos="left"
            :placeholder="this.$_MISAResources.text__combobox.type"
            :listOptions="listCategory"
            name="categoryName"
            v-model="categoryFilter"
            @change="changeCategory"
          ></m-combobox>
        </div>
        <div class="content__top--wrapper">
          <m-combobox
            iconPrefix="icon__filter"
            iconPos="left"
            :placeholder="this.$_MISAResources.text__combobox.department"
            :listOptions="listDepartment"
            name="departmentName"
            v-model="departmentFilter"
            @change="changeDepartment"
          ></m-combobox>
        </div>
        <div class="content__top--wrapper">
          <m-button
            iconButton="icon__reload"
            :content="this.$_MISAResources.content__button.reloadAsync"
            className="btn__reload"
            @click="handleReload"
          ></m-button>
        </div>
      </div>
      <div class="content__top--right">
        <m-button
          className="btn__add btn__main"
          iconButton="icon__plus"
          :content="this.$_MISAResources.content__button.addAsset"
          @click="handleOpenForm"
        >
        </m-button>
        <a :href="`${apiUrl}/Assets/Export`">
          <m-button
            className="btn__export"
            iconButton="icon__export"
            :title="this.$_MISAResources.tooltip__btn.excel"
            posTooltip="top"
          >
          </m-button>
        </a>
        <m-button
          className="btn__delete"
          iconButton="icon__delete--red"
          :title="this.$_MISAResources.tooltip__btn.delete"
          @click="handleDelete"
          posTooltip="top"
        >
        </m-button>
        <div
          class="wrapper__dropdown"
          v-esc="handleCloseSetting"
          v-clickOutside="handleCloseSetting"
        >
          <m-button
            className="btn__setting"
            iconButton="icon__setting"
            :title="this.$_MISAResources.tooltip__btn.setting"
            @click="handleOpenSetting"
            posTooltip="top"
          ></m-button>
          <div class="setting__dropdown" v-if="isShowSetting">
            <div class="setting__title">
              <div>{{ this.$_MISAResources.title__dropdown.display }}</div>
              <m-button
                className="btn__close--setting"
                iconButton="icon__close"
                :title="this.$_MISAResources.tooltip__btn.close"
                posTooltip="top"
                @click="handleCloseSetting"
              ></m-button>
            </div>
            <div class="display__list">
              <div
                class="display__item--wrap"
                v-for="(item, index) in this.$_MISAResources.setting__display"
                :key="index"
              >
                <div class="display__item">
                  {{ item }}
                  <m-input
                    type="checkbox"
                    className="table__checkbox"
                    name="ckbox"
                    id="ckbox"
                    @input="handleCheckDisplay(item)"
                    v-if="!isDisplay(item)"
                  ></m-input>
                  <div
                    v-else-if="isDisplay(item)"
                    class="checkbox icon__checked"
                    @click="handleUnCheck(item)"
                  ></div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
    <!-- Table -->
    <m-table
      :columns="col"
      :dataTable="assetList"
      :numberPage="currentPage"
      @getData="handleData"
      @showPopup="handleShowPopup"
      @nextPage="nextPage"
      @prevPage="prevPage"
      @pageSize="chosePageSize"
      @loading="setLoading"
    ></m-table>
    <!-- Form -->
    <asset-form
      :data="dataRecieved"
      :isShow="isShow"
      :newCode="newAssetCode"
      @showToast="handleShowToast"
      @openPopup="showPopup"
      @loading="setLoading"
    ></asset-form>
    <!-- Popup -->
    <m-dialog v-if="isShowPopup || isReload">
      <template #content v-if="isReload">
        <m-loading></m-loading>
      </template>
      <template #content v-else>
        <m-popup
          :type="typePopup"
          :dataPopup="dataRecieved"
          :dataForm="dataFormAsset"
          :errMessage="listMessage"
          icon="icon__warning"
          @cancel="handleCancel"
          @showToast="handleShowToast"
          @closeForm="handleCloseForm"
          :isShow="isShowPopup"
        ></m-popup>
      </template>
    </m-dialog>
    <!-- Toast Message -->
    <m-toast
      :type="typeToast"
      :content="contentToast"
      icon="icon__check"
      :isShow="isShowToast"
    ></m-toast>
  </div>
</template>

<script>
import { request } from "@/services/request";
import Enum from "@/utils/enum";
import { mapActions, mapState } from "vuex";

import MButton from "@/components/base/MButton.vue";
import MCombobox from "@/components/base/MCombobox.vue";
import MDiaglog from "@/components/base/MDiaglog.vue";
import MInput from "@/components/base/MInput.vue";
import MLoading from "@/components/base/MLoading.vue";
import MPopup from "@/components/base/MPopup.vue";
import MToast from "@/components/base/MToast.vue";
import MTable from "@/components/table/MTable.vue";
import AssetForm from "@/view/asset/AssetForm";

export default {
  name: "AssetList",
  components: {
    "m-button": MButton,
    "m-input": MInput,
    "asset-form": AssetForm,
    "m-table": MTable,
    "m-popup": MPopup,
    "m-toast": MToast,
    "m-dialog": MDiaglog,
    "m-combobox": MCombobox,
    "m-loading": MLoading,
  },

  data() {
    return {
      apiUrl: process.env.VUE_APP_API_URL,
      isShowPopup: false,
      isShowCMenu: false,
      isReload: false,
      isShowToast: false,
      isShowSetting: false,
      stateTable: false,
      dataRecieved: "",
      dataFormAsset: "",
      listMessage: null,
      positionCMenu: null,
      typeToast: null,
      typePopup: null,
      contentToast: null,
      col: [
        {
          title: "",
          checkbox: true,
          key: this.$_MISAResources.table.key.checkbox,
        },
        {
          title: this.$_MISAResources.table.title.order,
          key: this.$_MISAResources.table.key.order,
          tooltip: this.$_MISAResources.table.title.orderTooltip,
        },
        {
          title: this.$_MISAResources.table.title.assetCode,
          posLeft: true,
          key: this.$_MISAResources.table.key.assetCode,
        },
        {
          title: this.$_MISAResources.table.title.assetName,
          posLeft: true,
          width: 200,
          key: this.$_MISAResources.table.key.assetName,
        },
        {
          title: this.$_MISAResources.table.title.categoryName,
          posLeft: true,
          width: 200,
          key: this.$_MISAResources.table.key.categoryName,
        },
        {
          title: this.$_MISAResources.table.title.departmentName,
          posLeft: true,
          width: 200,
          key: this.$_MISAResources.table.key.departmentName,
        },
        {
          title: this.$_MISAResources.table.title.quantity,
          posLeft: false,
          key: this.$_MISAResources.table.key.quantity,
        },
        {
          title: this.$_MISAResources.table.title.cost,
          posLeft: false,
          key: this.$_MISAResources.table.key.cost,
        },
        {
          title: this.$_MISAResources.table.title.depreciationAmount,
          posLeft: false,
          key: this.$_MISAResources.table.key.depreciationAmount,
          tooltip: this.$_MISAResources.table.title.depreciationAmountTooltip,
        },
        {
          title: this.$_MISAResources.table.title.residualPrice,
          posLeft: false,
          key: this.$_MISAResources.table.key.residualPrice,
        },
        {
          title: this.$_MISAResources.table.title.trackedYear,
          center: true,
          key: this.$_MISAResources.table.key.trackedYear,
        },
      ],
      departmentFilter: null,
      categoryFilter: null,
      listDepartment: null,
      listCategory: null,
      inputSearch: null,
      debounce: null,
      linkExcel: null,
      listAsset: null,
      currentPage: 1,
      pageSize: 20,
      assetClone: null,
      newAssetCode: null,
      arrDisplay: ["Summary", "Paging"],
    };
  },

  created() {
    /**
     * Function call api from store
     * Author: HMDUC (28/05/2023)
     */
    this.getAssetList({
      pageNumber: this.currentPage,
      pageSize: this.pageSize,
    });

    /**
     * Function call api get list department
     * Author: HMDUC (28/05/2023)
     */
    this.getDepartmentList();

    /**
     * Function call api get list category
     * Author: HMDUC (28/05/2023)
     */
    this.getCategory();

    /**
     * Function call api get new AssetCode;
     * Author: HMDUC (28/05/2023)
     */
    this.getNewAssetCode();

    this.setListDisplayed(this.arrDisplay);
  },

  computed: {
    ...mapState("formDialog", ["isShow", "dataForm"]),
    ...mapState("asset", ["assetList", "listSelected", "isLoading"]),
    ...mapState("displayTable", ["listDisplayed"]),

    isDisplay() {
      return (field) => {
        return this.arrDisplay.find((item) => item === field);
      };
    },
  },

  methods: {
    ...mapActions("formDialog", ["setIsShow", "setDataForm", "setFormMode"]),
    ...mapActions("asset", ["deleteListSelected", "getAssetList"]),
    ...mapActions("displayTable", ["setListDisplayed"]),

    /**
     * Function call Api Get NewAssetCode
     * Author: HMDUC(15/06/2023)
     */
    async getNewAssetCode() {
      try {
        const res = await request.get(`/Assets/NewCode`);
        this.newAssetCode = res.toString();
      } catch (err) {
        this.$emit(
          "showToast",
          "notice",
          this.$_MISAResources.toast__content.ErrorServer
        );
      }
    },

    /**
     * Function call Api get Department list
     * Author: HMDUC (15/06/2023)
     */
    async getDepartmentList() {
      try {
        const res = await request.get(`/Departments`);
        const mapData = res.map((item) => ({
          label: item.DepartmentName,
          value: item.DepartmentName,
        }));

        this.listDepartment = [
          {
            label: "-- Chọn bộ phận sử dụng --",
            value: "",
          },
          ...mapData,
        ];
      } catch (err) {
        this.isShowToast = true;
        this.typeToast = "notice";
        this.contentToast = this.$_MISAResources.toast__content.ErrorServer;
      }
    },

    /**
     * Function call Api Category list
     * Author: HMDUC (15/06/2023)
     */
    async getCategory() {
      try {
        const res = await request.get(`/Category`);
        const mapData = res.map((item) => ({
          label: item.CategoryName,
          value: item.CategoryName,
        }));
        this.listCategory = [
          {
            label: "-- Chọn loại tài sản --",
            value: "",
          },
          ...mapData,
        ];
      } catch (err) {
        this.isShowToast = true;
        this.typeToast = "notice";
        this.contentToast = this.$_MISAResources.toast__content.ErrorServer;
      }
    },

    /**
     * Function set Loading
     * Author: HMDUC (16/07/2023)
     */
    setLoading(isLoading) {
      if (isLoading) {
        this.isReload = isLoading;
      } else {
        setTimeout(() => {
          this.isReload = isLoading;
        }, 300);
      }
    },

    /**
     * Function filter category
     * Author: HMDUC (15/06/2023)
     */
    changeCategory() {
      this.currentPage = 1;
      this.getAssetList({
        pageNumber: this.currentPage,
        pageSize: this.pageSize,
        searchInput: this.inputSearch,
        categoryName: this.categoryFilter,
        departmentName: this.departmentFilter,
      });
    },

    /**
     * Function filter department
     * Author: HMDUC (15/06/2023)
     */
    changeDepartment() {
      this.currentPage = 1;
      this.getAssetList({
        pageNumber: this.currentPage,
        pageSize: this.pageSize,
        searchInput: this.inputSearch,
        categoryName: this.categoryFilter,
        departmentName: this.departmentFilter,
      });
    },

    /**
     * Function handle Search By Name or Code
     * Author: HMDUC (15/06/2023)
     */
    handleSearch() {
      this.currentPage = 1;
      this.getAssetList({
        pageNumber: this.currentPage,
        pageSize: this.pageSize,
        searchInput: this.inputSearch,
        categoryName: this.categoryFilter,
        departmentName: this.departmentFilter,
      });
    },

    clearSearch() {
      if (this.inputSearch.length === 0) {
        clearTimeout(this.debounce);
        this.debounce = setTimeout(() => {
          this.getAssetList({
            pageNumber: 1,
            pageSize: this.pageSize,
            searchInput: this.inputSearch,
            categoryName: this.categoryFilter,
            departmentName: this.departmentFilter,
          });
        }, 800);
      }
    },

    /**
     * Function chose pageSize
     * @param {*} pageSize
     * Author: HMDUC (15/06/2023)
     */
    chosePageSize(pageSize) {
      this.pageSize = pageSize;
      this.getAssetList({
        pageNumber: 1,
        pageSize: this.pageSize,
        searchInput: this.inputSearch,
        categoryName: this.categoryFilter,
        departmentName: this.departmentFilter,
      });
    },

    /**
     * Function handle reload
     * Author: HMDUC (15/06/2023)
     */
    handleReload() {
      this.isReload = true;
      setTimeout(() => {
        this.currentPage = 1;
        this.departmentFilter = null;
        this.categoryFilter = null;
        this.inputSearch = "";
        this.pageSize = 20;
        this.isReload = false;
        this.getAssetList({
          pageNumber: this.currentPage,
          pageSize: this.pageSize,
          searchInput: null,
          categoryName: null,
          departmentName: null,
        });
      }, 1000);
    },

    /**
     * Function handle next page
     * Author: HMDUC (15/06/2023)
     */
    nextPage(page) {
      this.currentPage = page;
      this.getAssetList({
        pageNumber: this.currentPage,
        pageSize: this.pageSize,
        searchInput: this.inputSearch,
        categoryName: this.categoryFilter,
        departmentName: this.departmentFilter,
      });
    },

    /**
     * Function handle prev Page
     * Author: HMDUC (15/06/2023)
     */
    prevPage(page) {
      this.currentPage = page;
      this.getAssetList({
        pageNumber: this.currentPage,
        pageSize: this.pageSize,
        searchInput: this.inputSearch,
        categoryName: this.categoryFilter,
        departmentName: this.departmentFilter,
      });
    },

    /**
     * Function open setting dropdown
     * Author: HMDUC (18/07/2023)
     */
    handleOpenSetting() {
      this.isShowSetting = !this.isShowSetting;
    },

    /**
     * Function close setting dropdown
     * Author: HMDUC (18/07/2023)
     */
    handleCloseSetting() {
      this.isShowSetting = false;
    },

    /**
     * Function  set filed display
     * Author: HMDUC (18/07/2023)
     * @param {*} field
     */
    handleCheckDisplay(field) {
      if (!this.arrDisplay.some((item) => item === field)) {
        this.arrDisplay.push(field);
      } else {
        this.arrDisplay = this.arrDisplay.filter((item) => item !== field);
      }
      this.setListDisplayed(this.arrDisplay);
    },

    /**
     * Function remove filed display
     * Author: HMDUC (18/07/2023)
     * @param {*} field
     */
    handleUnCheck(field) {
      const index = this.arrDisplay.indexOf(field);
      this.arrDisplay.splice(index, 1);
      this.setListDisplayed(this.arrDisplay);
    },

    /**
     * Function handle delete multiple asset is selected by chk
     * Author: HMDUC(27/05/2023)
     */
    handleDelete() {
      //delelte asset selected by checkbox
      let lengthList = this.listSelected.length;
      switch (lengthList) {
        case 0:
          this.typeToast = "warning";
          this.isShowToast = true;
          setTimeout(() => {
            this.isShowToast = false;
          }, 3200);
          break;
        default:
          this.handleShowPopup(this.listSelected, "confirm");
          break;
      }
    },

    /**
     * Function open form add
     * Author: HMDUC(27/05/2023)
     */
    handleOpenForm() {
      this.isReload = true;
      setTimeout(() => {
        this.isReload = false;
        this.dataRecieved = null;
        this.setIsShow(true);
        this.setFormMode(Enum.FORM__MODE.ADD);
      }, 100);
    },

    /**
     * Function close form add
     * Author: HMDUC(27/05/2023)
     */
    handleCloseForm() {
      this.setIsShow(false);
      this.isShowPopup = false;
    },

    /**
     * Function emit data asset when selected
     * Author: HMDUC(27/05/2023)
     */
    handleData(data) {
      this.dataRecieved = data;
    },

    /**
     * Function show popup delete
     * Author: HMDUC(27/05/2023)
     * @param {*} data
     * @param {*} type
     */
    handleShowPopup(data, type) {
      let listJson = JSON.stringify(data);
      this.typePopup = type;
      this.dataRecieved = JSON.parse(listJson);
      this.isShowPopup = true;
    },

    /**
     * Funtion show popup when edit
     * Author: HMDUC(27/05/2023)
     * @param {*} type
     * @param {*} data
     */
    showPopup(type, data) {
      if (type == "warning") {
        this.dataFormAsset = data;
      } else {
        this.listMessage = data;
      }
      this.typePopup = type;
      this.isShowPopup = true;
    },

    /**
     * Function close popup
     * Author: HMDUC(27/05/2023)
     */
    handleCancel() {
      this.isShowPopup = false;
    },

    /**
     * Function show toast , emit type to component toast
     * Author: HMDUC(27/05/2023)
     * @param {*} type
     * @param {*} content
     */
    handleShowToast(type, content) {
      this.typeToast = type;
      this.contentToast = content;
      this.isShowToast = true;
      this.isShowPopup = false;
      setTimeout(() => {
        this.isShowToast = false;
      }, 3200);
    },
  },
};
</script>

<style>
@import "@/css/base/table.css";
@import "@/css/base/content.css";
@import "@/css/base/combobox.css";
@import "@/css/base/input.css";
@import "@/css/base/dialog.css";
@import "@/css/base/dropdown.css";
</style>
