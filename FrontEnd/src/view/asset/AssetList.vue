<template>
  <div class="main__content">
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
            @input="handleSearch(inputSearch)"
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
            :content="this.$_MISAResources.content__button.reload_async"
            className="btn__reload"
            @click="handleReload"
          ></m-button>
        </div>
      </div>
      <div class="content__top--right">
        <m-button
          className="btn__add btn__main"
          iconButton="icon__plus"
          :content="this.$_MISAResources.content__button.add_asset"
          @click="handleOpenForm"
        >
        </m-button>
        <a href="https://localhost:7050/api/v1/Assets/Export">
          <m-button
            className="btn__export"
            iconButton="icon__export"
            :title="this.$_MISAResources.tooltip__btn.excel"
          >
          </m-button>
        </a>
        <m-button
          className="btn__delete"
          iconButton="icon__delete--red"
          :title="this.$_MISAResources.tooltip__btn.delete"
          @click="handleDelete"
        >
        </m-button>
      </div>
    </div>
    <m-table
      :columns="col"
      :dataTable="propertyList"
      @showCMenu="showContextMenu"
      @getData="handleData"
      @showPopup="handleShowPopup"
      @nextPage="nextPage"
      @prevPage="prevPage"
      @pageSize="chosePageSize"
    ></m-table>
    <asset-form
      :data="dataRecieved"
      :isShow="isShow"
      @showToast="handleShowToast"
      @openPopup="openPopupCancel"
    ></asset-form>
    <m-dialog v-if="isShowPopup || isReload">
      <template #content v-if="isReload">
        <m-loading></m-loading>
      </template>
      <template #content v-else>
        <m-popup
          :type="typePopup"
          :dataPopup="dataRecieved"
          icon="icon__warning"
          @cancel="handleCancel"
          @showToast="handleShowToast"
          @closeForm="handleCloseForm"
        ></m-popup>
      </template>
    </m-dialog>
    <m-toast
      :type="typeToast"
      :content="contentToast"
      icon="icon__check"
      :isShow="isShowToast"
    ></m-toast>
  </div>
</template>

<script>
import { mapActions, mapState } from "vuex";
import Enum from "@/utils/enum";
import { request } from "@/services/request";

import MButton from "@/components/base/MButton.vue";
import MTable from "@/components/table/MTable.vue";
import MInput from "@/components/base/MInput.vue";
import MPopup from "@/components/base/MPopup.vue";
import MDiaglog from "@/components/base/MDiaglog.vue";
import MToast from "@/components/base/MToast.vue";
import MCombobox from "@/components/base/MCombobox.vue";
import MLoading from "@/components/base/MLoading.vue";
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
      isShowPopup: false,
      isShowCMenu: false,
      isReload: false,
      isShowToast: false,
      stateTable: false,
      dataRecieved: "",
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
    };
  },

  created() {
    /**
     * function call api from store
     * Author: HMDUC (28/05/2023)
     */
    this.getPropertyList({
      pageNumber: this.currentPage,
      pageSize: this.pageSize,
    });
    this.getDepartmentList();
    this.getCategory();
    this.getExport();
  },

  computed: {
    ...mapState("formDialog", ["isShow", "dataForm"]),
    ...mapState("property", [
      "propertyList",
      "listSelected",
      "loadingTime",
      "isLoading",
    ]),
  },

  methods: {
    ...mapActions("formDialog", ["setIsShow", "setDataForm", "setFormMode"]),

    ...mapActions("property", ["deleteListSelected", "getPropertyList"]),

    async getExport() {
      try {
        const res = await request.get(`/Assets/Export`);
        this.linkExcel = res;
        return res;
      } catch (e) {
        console.log(e);
      }
    },

    /**
     * Function call Api get Departmen list
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
     * Function filter category
     * Author: HMDUC (15/06/2023)
     */
    changeCategory() {
      this.getPropertyList({
        pageNumber: 1,
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
      this.getPropertyList({
        pageNumber: 1,
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
      clearTimeout(this.debounce);
      this.debounce = setTimeout(() => {
        this.getPropertyList({
          pageNumber: 1,
          pageSize: this.pageSize,
          searchInput: this.inputSearch,
          categoryName: this.categoryFilter,
          departmentName: this.departmentFilter,
        });
      }, 800);
    },

    /**
     * Function chose pageSize
     * @param {*} pageSize
     * Author: HMDUC (15/06/2023)
     */
    chosePageSize(pageSize) {
      this.getPropertyList({
        pageNumber: 1,
        pageSize: pageSize,
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
        this.isReload = false;
        this.prevPage(1);
      }, 1000);
    },

    /**
     * Function handle next page
     * Author: HMDUC (15/06/2023)
     */
    nextPage(page) {
      this.currentPage++;
      this.getPropertyList({
        pageNumber: page,
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
      this.currentPage--;
      this.getPropertyList({
        pageNumber: page,
        pageSize: this.pageSize,
        searchInput: this.inputSearch,
        categoryName: this.categoryFilter,
        departmentName: this.departmentFilter,
      });
    },

    /**
     * function handle delete multiple property is selected by chk
     * Author: HMDUC(27/05/2023)
     */
    handleDelete() {
      //delelte property selected by checkbox
      let lengthList = this.listSelected.length;
      switch (lengthList) {
        case 0:
          this.typeToast = "warning";
          this.isShowToast = true;
          setTimeout(() => {
            this.isShowToast = false;
            this.stateTable = true;
          }, 2500);
          break;
        default:
          this.handleShowPopup(this.listSelected, "confirm");
          break;
      }
    },

    /**
     * function open form add
     * Author: HMDUC(27/05/2023)
     */
    handleOpenForm() {
      this.dataRecieved = null;
      this.setIsShow(true);
      this.setFormMode(Enum.FORM__MODE.ADD);
    },

    /**
     * function close form add
     * Author: HMDUC(27/05/2023)
     */
    handleCloseForm() {
      this.setIsShow(false);
      this.isShowPopup = false;
    },

    /**
     * function emit data property when selected
     * Author: HMDUC(27/05/2023)
     */
    handleData(data) {
      this.dataRecieved = data;
      this.setIsShow(true);
      this.setFormMode(Enum.FORM__MODE.EDIT);
    },

    /**
     * function open popup
     * Author: HMDUC(27/05/2023)
     */
    handleShowPopup(data, type) {
      this.typePopup = type;
      this.dataRecieved = data;
      this.isShowPopup = true;
    },

    openPopupCancel(type, data) {
      this.typePopup = type;
      this.isShowPopup = true;
      this.dataRecieved = data;
    },

    /**
     * function close popup
     * Author: HMDUC(27/05/2023)
     */
    handleCancel() {
      this.isShowPopup = false;
    },

    /**
     * function show toast , emit type to component toast
     * Author: HMDUC(27/05/2023)
     * @param {*} type
     */
    handleShowToast(type, content) {
      this.isShowPopup = false;
      this.typeToast = type;
      this.contentToast = content;
      this.isShowToast = true;
      setTimeout(() => {
        this.isShowToast = false;
        this.stateTable = true;
      }, 2500);
    },

    /**
     * function show context menu when click right mouse
     * Author: HMDUC(27/05/2023)
     * @param {*} data
     * @param {*} posMenu
     */
    showContextMenu(data, posMenu) {
      this.dataRecieved = data;
      this.isShowCMenu = true;
      this.positionCMenu = posMenu;
    },
    /**
     *  function show popup to confirm delete
     *  Author: HMDUC(27/05/2023)
     * @param {*} data
     */
    showPopup(data) {
      this.dataRecieved = data;
      this.isShowPopup = true;
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
</style>
