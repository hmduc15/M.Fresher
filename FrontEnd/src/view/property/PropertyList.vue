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
            icon="icon__filter"
            iconPos="left"
            :placeholder="this.$_MISAResources.text__combobox.type"
            :listOptions="listCategory"
            name="categoryName"
            v-model="categoryFilter"
            @change="changeCategory"
          ></m-combobox>
          <div class="select__icon--r select__icon icon__down"></div>
        </div>
        <div class="content__top--wrapper">
          <m-combobox
            icon="icon__filter"
            iconPos="left"
            :placeholder="this.$_MISAResources.text__combobox.department"
            :listOptions="listDepartment"
            name="departmentName"
            v-model="departmentFilter"
            @change="changeDepartment"
          ></m-combobox>
          <div class="select__icon--r select__icon icon__down"></div>
        </div>
      </div>
      <div class="content__top--right">
        <m-button
          className="btn__add btn__main"
          iconButton="icon__plus"
          content="Thêm tài sản"
          @click="handleOpenForm"
        >
        </m-button>
        <m-button
          className="btn__export"
          iconButton="icon__export"
          title="Xuất file excel"
        >
        </m-button>
        <m-button
          className="btn__delete"
          iconButton="icon__delete--red"
          title="Xoá tài sản"
          @click="handelDelete"
        >
        </m-button>
      </div>
    </div>
    <m-table
      :columns="col"
      :dataTable="propertyList"
      @showCMenu="showContextMenu"
      @getData="handleData"
      @showPopup="this.handleShowPopup"
      @nextPage="nextPage"
      @prevPage="prevPage"
      @pageSize="chosePageSize"
    ></m-table>
    <property-form
      :data="dataRecieved"
      :isShow="isShow"
      @showToast="handleShowToast"
    ></property-form>
    <m-dialog v-if="isShowPopup">
      <template #content>
        <m-popup
          :dataPopup="dataRecieved"
          icon="icon__warning"
          @cancel="handleCancel"
          @showToast="handleShowToast"
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
import PropertyForm from "./PropertyForm.vue";

export default {
  name: "PropertyPage",
  components: {
    MTable,
    "m-button": MButton,
    "m-input": MInput,
    "property-form": PropertyForm,
    "m-table": MTable,
    "m-popup": MPopup,
    "m-toast": MToast,
    "m-dialog": MDiaglog,
    "m-combobox": MCombobox,
  },

  data() {
    return {
      isShowPopup: false,
      isShowCMenu: false,
      isShowToast: false,
      stateTable: false,
      dataRecieved: "",
      positionCMenu: null,
      typeToast: null,
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
          title: this.$_MISAResources.table.title.depreciationRate,
          posLeft: false,
          key: this.$_MISAResources.table.key.depreciationRate,
          tooltip: this.$_MISAResources.table.title.depreciationRateTooltip,
        },
        {
          title: this.$_MISAResources.table.title.pricedUsed,
          posLeft: false,
          key: this.$_MISAResources.table.key.cost,
        },
        {
          title: this.$_MISAResources.table.title.trackedYear,
          center: true,
          key: this.$_MISAResources.table.key.trackedYear,
        },
        {},
      ],
      departmentFilter: null,
      categoryFilter: null,
      listDepartment: null,
      listCategory: null,
      inputSearch: null,
      debounce: null,
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
        console.log(err);
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
        console.log(err);
      }
    },

    changeCategory() {
      this.getPropertyList({
        pageNumber: 1,
        pageSize: this.pageSize,
        searchInput: this.inputSearch,
        categoryName: this.categoryFilter,
        departmentName: this.departmentFilter,
      });
    },

    changeDepartment() {
      this.getPropertyList({
        pageNumber: 1,
        pageSize: this.pageSize,
        searchInput: this.inputSearch,
        categoryName: this.categoryFilter,
        departmentName: this.departmentFilter,
      });
    },

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

    chosePageSize(pageSize) {
      this.getPropertyList({
        pageNumber: this.currentPage,
        pageSize: pageSize,
        searchInput: this.inputSearch,
        categoryName: this.categoryFilter,
        departmentName: this.departmentFilter,
      });
    },

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
    handelDelete() {
      //delelte property selected by checkbox
      let lengthList = this.listSelected.length;
      console.log(this.listSelected);
      switch (lengthList) {
        case 0:
          this.typeToast = "warning";
          this.isShowToast = true;
          setTimeout(() => {
            this.isShowToast = false;
            this.stateTable = true;
          }, 1500);
          break;
        default:
          this.handleShowPopup(this.listSelected);
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
      this.isShowForm = false;
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
    handleShowPopup(data) {
      this.dataRecieved = data;
      this.isShowPopup = true;
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
      this.typeToast = type;
      this.contentToast = content;
      this.isShowPopup = false;
      this.isShowToast = true;
      setTimeout(() => {
        this.isShowToast = false;
        this.stateTable = true;
      }, 1500);
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
