<template>
  <div ref="tableRef" class="table__view" v-move-row="{ moveRow }">
    <div
      @mousemove.stop="handleResize"
      @mouseup.capture="handleStopResize"
      class="table__container"
    >
      <div class="table__content">
        <div class="table__content--header">
          <div class="header__row">
            <div
              class="header__row--item resizable"
              v-for="(column, index) in columns"
              :key="index"
              :style="[`width: ${widthDefault[index]}px`]"
              :class="[
                `${column.posLeft ? 'align--left' : 'align--right'}`,
                `${column.action ? 'align--center' : ''}`,
                `${column.checkbox ? 'align--center' : ''}`,
                `${column.key === 'order' ? 'align--center' : ''}`,
              ]"
            >
              <template v-if="column.key === 'checkbox'">
                <div class="ceil--checkbox">
                  <m-input
                    v-if="!isCheckAll"
                    type="checkbox"
                    className="table__checkbox"
                    name="ckbox"
                    id="ckbox"
                    @input="handleCheckAll"
                    @click.stop
                  ></m-input>
                  <div
                    v-else
                    class="checkbox icon__checked"
                    @click.stop="handleUnCheckAll"
                  ></div>
                </div>
              </template>
              <template v-else-if="column.title && !column.tooltip">
                <span>
                  {{ column.title }}
                </span>
              </template>
              <template v-else-if="column.title && column.tooltip">
                <m-tooltip
                  :content="column.tooltip"
                  placement="top"
                  effect="dark"
                >
                  <template #child>
                    <span>
                      {{ column.title }}
                    </span>
                  </template>
                </m-tooltip>
              </template>
              <template v-else> </template>
              <div
                class="resize--handle"
                @mousedown.stop="handleStartResize(index)"
              ></div>
            </div>
          </div>
        </div>
        <div
          class="table__content--body"
          ref="tBody"
          @scroll="hanldeScroll($event)"
        >
          <div
            class="row--item"
            ref="rowItem"
            :row-index="indexRow"
            :class="[
              'table__body--row',
              { 'table--active': isRowActive(indexRow) },
              {
                'table--selected': isSelected(data),
              },
              {
                'row--hovered': this.indexRowHover === indexRow,
              },
            ]"
            v-for="(data, indexRow) in assetClone"
            :key="indexRow"
            @contextmenu.prevent="showContextMenu($event, data)"
            @click="handleClickRow(data, indexRow, $event)"
            @mouseenter.stop="handleHoverStart(indexRow)"
            @mouseleave.stop="handleHoverEnd"
            @dblclick="handleOpenEdit(data)"
          >
            <div
              class="ceil--item"
              v-for="(column, index) in columns"
              :key="index"
              :class="[
                `${column.posLeft ? 'align--left' : 'align--right'}`,
                `${column.action ? ' align--center' : ''}`,
                `${column.checkbox ? 'align--center' : ''}`,
                `${column.key === 'order' ? 'align--center' : ''}`,
              ]"
              :style="[`width: ${widthDefault[index]}px`]"
            >
              <template v-if="column.key === 'checkbox'">
                <div class="ceil--checkbox">
                  <m-input
                    v-if="!isSelected(data)"
                    type="checkbox"
                    className="table__checkbox"
                    name="ckbox"
                    id="ckbox"
                    :value="data.AssetCode"
                    @input="handleChecked(data, $event, indexRow)"
                    @click.stop
                  ></m-input>
                  <div
                    v-else-if="isSelected(data)"
                    class="checkbox icon__checked"
                    @click.stop="handleUnCheck(data)"
                  ></div>
                </div>
              </template>
              <template v-else-if="column.key === 'order'">
                <span>{{ indexRow + 1 }}</span>
              </template>

              <template v-else>
                <span>{{ data[column.key] }}</span>
              </template>
            </div>
          </div>
        </div>
        <div class="table__content--summary">
          <div
            class="ceil--item"
            v-for="(column, index) in columns"
            :key="index"
            :class="[
              `${column.posLeft ? 'align--left' : 'align--right'}`,
              `${column.action ? ' align--center' : ''}`,
              `${column.checkbox ? 'align--center' : ''}`,
              `${column.key === 'order' ? 'align--center' : ''}`,
            ]"
            :style="[`width: ${widthDefault[index]}px`]"
          >
            <template v-if="column.key === 'Quantity'">
              <span>{{
                this.formatValue(this.dataSummary.total_quantity)
              }}</span>
            </template>
            <template v-if="column.key === 'Cost'">
              <span>{{ this.formatValue(this.dataSummary.total_cost) }}</span>
            </template>
            <template v-if="column.key === 'DepreciationAmount'">
              <span>{{
                this.formatValue(this.dataSummary.total_depreciation)
              }}</span>
            </template>
            <template v-if="column.key === 'ResidualPrice'">
              <span>{{
                this.formatValue(this.dataSummary.total_residual_price)
              }}</span>
            </template>
          </div>
        </div>
      </div>
      <div class="table__paging">
        <div class="total_record">
          {{ this.$_MISAResources.paging.total + ":" }}
          <b>{{ totalRecored }}</b>
          {{ this.$_MISAResources.paging.record }}
        </div>
        <div class="page__size">
          <m-combobox
            iconPrefix="icon__down--page"
            :listOptions="listOffset"
            v-model="this.pageSize"
            @change="chosePageSize"
          ></m-combobox>
        </div>
        <div class="pagination">
          <m-tooltip :content="this.$_MISAResources.tooltip__btn.firstPage">
            <template #child>
              <button
                :class="[
                  { 'btn--notallowed': this.currentPage === 1 },
                  { 'btn--hover': this.currentPage > 1 },
                  'btn__paging',
                ]"
                @click="firstPage"
              >
                <div class="icon__prev"></div>
                <div class="icon__prev"></div>
              </button>
            </template>
          </m-tooltip>
          <m-tooltip :content="this.$_MISAResources.tooltip__btn.prevPage">
            <template #child>
              <m-button
                className="btn__prev"
                iconButton="icon__prev"
                @click="prevPage"
              ></m-button>
            </template>
          </m-tooltip>
          <div class="page__container">
            <m-input
              type="text"
              v-model="pageInput"
              :className="['input__paging']"
              @input="pageInput = this.handleInputPage(pageInput)"
            ></m-input>
          </div>
          <m-tooltip :content="this.$_MISAResources.tooltip__btn.nextPage">
            <template #child>
              <m-button
                className="btn__next"
                iconButton="icon__next"
                @click="nextPage"
              >
              </m-button>
            </template>
          </m-tooltip>
          <m-tooltip :content="this.$_MISAResources.tooltip__btn.lastPage">
            <template #child>
              <button
                :class="[
                  { 'btn--notallowed': this.currentPage === this.totalPage },
                  { 'btn--hover': this.currentPage < this.totalPage },
                  'btn__paging',
                ]"
                @click="lastPage"
              >
                <div class="icon__next"></div>
                <div class="icon__next"></div>
              </button>
            </template>
          </m-tooltip>
        </div>
      </div>
      <div
        class="empty__data icon--empty"
        v-if="dataTable.data?.length === 0 && !isLoading"
      ></div>
      <div class="fixed__content">
        <div class="fixed__content--header">
          <span>{{ this.$_MISAResources.table.title.action }}</span>
        </div>
        <div
          class="fixed__content--body"
          ref="fBody"
          @scroll="hanldeScroll($event)"
        >
          <!-- Loading -->
          <div
            class="fixed__row--item"
            v-for="(data, indexRow) in assetClone"
            :key="indexRow"
            :row-index="indexRow"
            :class="[
              {
                'table--active': isRowActive(indexRow),
                'table--selected': isSelected(data),
                'row--hovered': this.indexRowHover === indexRow,
              },
            ]"
            @click="handleClickRow(data, indexRow, $event)"
            @mouseenter="handleHoverStart(indexRow)"
            @mouseleave="handleHoverEnd"
          >
            <div class="fixed__ceil--item">
              <m-button
                className="btn__row btn__edit"
                iconButton="icon__edit"
                @click="openForm(data, $event)"
                :title="this.$_MISAResources.tooltip__btn.edit"
                posTooltip="left"
              ></m-button>
              <m-button
                className="btn__row btn__message"
                iconButton="icon__duplicate"
                :title="this.$_MISAResources.tooltip__btn.delete"
                posTooltip="top"
                @click="handleDuplicate(data)"
              ></m-button>
            </div>
          </div>
        </div>
        <div class="fixed__content--summary"></div>
      </div>
      <div class="loading__container" v-if="isLoading">
        <div class="row--item" v-for="index in rowLoading" :key="index">
          <m-skeleton :isLoading="true" :rows="0"></m-skeleton>
        </div>
      </div>
    </div>
    <div
      ref="menu"
      :style="{
        top: pos.y + 5 + 'px',
        left: pos.x - 60 + 'px',
      }"
      class="context__menu"
      v-show="isShowMenu"
    >
      <div class="context__button">
        <m-button
          className="btn__context"
          iconButton="icon__plus--bl"
          content="Thêm"
          @click="handleBtnAdd"
        >
        </m-button>
        <m-button
          className="btn__context"
          iconButton="icon__edit"
          content="Sửa"
          @click="handleBtnEdit(dataSelected)"
        >
        </m-button>
        <m-button
          className="btn__context"
          iconButton="icon__delete--black"
          content="Xóa"
          @click="handleBtnDelete(dataSelected)"
        >
        </m-button>
        <m-button
          className="btn__context"
          iconButton="icon__duplicate"
          content="Nhân bản"
          @click="handleBtnDuplicate(dataSelected)"
        >
        </m-button>
      </div>
    </div>
  </div>
</template>

<script>
import { mapActions, mapState } from "vuex";

import { format } from "@/utils/format";
import Enum from "@/utils/enum";

import { directive } from "vue-tippy";
import MSkeleton from "../base/MSkeleton.vue";
import MTooltip from "../base/MTooltip.vue";
import MInput from "../base/MInput.vue";
import MCombobox from "../base/MCombobox.vue";
import MButton from "../base/MButton.vue";

export default {
  name: "MTable",
  props: ["columns", "state", "dataTable"],

  directives: {
    tippy: directive,
  },

  components: {
    // TableHeader,
    "m-button": MButton,
    "m-input": MInput,
    "m-tooltip": MTooltip,
    "m-combobox": MCombobox,
    "m-skeleton": MSkeleton,
  },

  data() {
    return {
      assetClone: [],
      dataSummary: {},
      posMenu: { x: 0, y: 0 },
      dataRow: null,
      dataSelected: null,
      dataDislay: null,
      isShowPopup: false,
      currentPage: 1,
      pageSize: 20,
      arrSelected: [],
      isChecked: false,
      isCheckAll: false,
      isActive: true,
      indexActive: -1,
      indexRowClick: -1,
      indexShiftClick: -1,
      indexCtrlClick: -1,
      indexRowHover: -1,
      isShiftClick: false,
      isDbClick: false,
      pageInput: 1,
      listOffset: [
        {
          value: 20,
          label: 20,
        },
        {
          value: 40,
          label: 40,
        },
        {
          value: 60,
          label: 60,
        },
      ],
      rowLoading: 12,
      widthDefault: [45, 50, 110, 200, 180, 170, 80, 140, 110, 130, 110, 50],
      indexResize: null,
      startX: 0,
      startWidth: 0,
      minWidth: 100,
      debounce: null,
    };
  },

  mounted() {
    this.$nextTick(() => {
      this.widthSkeleton = this.$refs.tableRef?.offsetWidth;
    });
  },

  watch: {
    reLoad(newValue) {
      if (newValue) {
        this.currentPage = 1;
      }
    },

    currentPage(newPage) {
      this.pageInput = newPage;
    },

    dataTable(newData) {
      this.assetClone = newData.data.map((item) => {
        return {
          ...item,
          Cost: this.formatValue(item.Cost),
          Quantity: this.formatValue(item.Quantity),
          DepreciationAmount: this.formatValue(
            Math.round(item.DepreciationAmount)
          ),
          DepreciationYear: this.formatValue(Math.round(item.DepreciationYear)),

          ResidualPrice: this.formatValue(Math.round(item.ResidualPrice)),
        };
      });
      this.dataSummary = newData.summaryData;
    },
  },

  computed: {
    ...mapState("formDialog", ["isShow"]),
    ...mapState("contextMenu", ["isShowMenu", "dataCMenu", "pos"]),
    ...mapState("property", [
      "propertyList",
      "listSelected",
      "loadingTime",
      "isLoading",
      "reLoad",
    ]),

    /**
     * Function check selected row
     * Author:  HMDUC (25/06/2023)
     */
    isSelected() {
      return (data) => {
        return this.listSelected.find(
          (asset) => asset.AssetId === data.AssetId
        );
      };
    },

    /**
     * Function check active row
     * Author:  HMDUC (25/06/2023)
     */
    isRowActive() {
      return (index) => {
        return this.indexActive === index;
      };
    },

    /**
     * Function return totalrecord
     * Author: HMDUC(26/05/2023)
     */
    paginateTable() {
      return this.propertyList.data;
    },

    /**
     * Function return total record
     * Author: HMDUC(26/05/2023)
     */
    totalRecored() {
      return this.propertyList.totalRow;
    },
    /**
     * Function return total page
     * Author: HMDUC(26/05/2023)
     */
    totalPage() {
      return Math.ceil(this.propertyList.totalRow / this.pageSize);
    },
  },

  methods: {
    ...mapActions("formDialog", ["setIsShow", "setDataForm", "setFormMode"]),
    ...mapActions("contextMenu", ["setShowMenu", "setDataMenu", "setPos"]),
    ...mapActions("property", [
      "getPropertyList",
      "deleteProperty",
      "setListSelected",
    ]),

    /**
     * funtion handle start resize event
     * @param {*} index
     * Author: HMDUC (20/06/2023)
     */
    handleStartResize(index) {
      this.indexResize = index;
      this.startX = event.pageX;
      this.startWidth = this.widthDefault[index];
    },

    /**
     * Function handle resize event
     * @param {} event
     * Author: HMDUC (20/06/2023)
     */
    handleResize(event) {
      if (this.indexResize != null) {
        const width = event.pageX - this.startX;
        this.widthDefault[this.indexResize] = this.startWidth + width;
        if (this.widthDefault[this.indexResize] < this.minWidth) {
          this.widthDefault[this.indexResize] = this.minWidth;
        }
      }
    },

    /**
     * Function handle stop resize event
     * Author: HMDUC (20/06/2023)
     */
    handleStopResize() {
      this.indexResize = null;
      this.startX = 0;
      this.startWidth = 0;
    },

    /**
     * Function handle mount start event
     * Author: HMDUC (18/06/2023)
     */
    handleHoverStart(index) {
      this.indexRowHover = index;
    },

    /**
     * Function handle mount end event
     * Author: HMDUC (18/06/2023)
     */
    handleHoverEnd() {
      this.indexRowHover = -1;
    },

    /**
     * Function handle Scroll event
     * Author: HMDUC (16/06/2023)
     * @param {*} e
     */
    hanldeScroll(e) {
      const tBody = this.$refs.tBody;
      const fBody = this.$refs.fBody;

      if (e.target == tBody) {
        fBody.scrollTop = tBody.scrollTop;
      } else {
        tBody.scrollTop = fBody.scrollTop;
      }
    },

    /**
     * Function format money before binding Ui
     * Author: HMDUC (10/06/2023)
     * @param {*} value
     * @returns 155555 -> 155.555
     */
    formatValue(value) {
      let money = format.formatMoney(value);
      return money;
    },

    /**
     * Function format number float
     * Author: HMDUC (10/06/2023)
     * @param {*} value
     * @returns 2.33 -> 2,33
     */
    formatFloat(value) {
      let float = format.formatFloat(value);
      return float;
    },

    /**
     * function handle event click
     * Author: HMDUC (09/06/2023)
     * @param {*} data
     * @param {*} index
     * @param {*} event
     */
    handleClickRow(data, index, event) {
      if (event.ctrlKey) {
        //  event ctrl + click
        this.handleSelected(data, index, event);
      } else if (event.shiftKey) {
        //event shift + click
        this.handleShiftClick(index, event);
      } else if (this.isShiftClick) {
        this.arrSelected = this.arrSelected.filter(
          (item) => item.AssetCode === data.AssetCode
        );
        this.setListSelected(this.arrSelected);
        this.isShiftClick = false;
        this.indexCtrlClick = index;
      } else {
        //normal click
        this.handleSelected(data, index, event);
      }
    },
    /**
     * function handle event ctrl  + click
     * Author: HMDUC (08/06/2023)
     * @param {*} data
     */
    handleSelected(data, index, e) {
      e.preventDefault();
      this.indexCtrlClick = index;
      if (!this.arrSelected.includes(data)) {
        this.arrSelected.push(data);
      } else {
        const index = this.arrSelected.indexOf(data);
        this.arrSelected.splice(index, 1);
      }
      this.setListSelected(this.arrSelected);
    },

    /**
     * Funtion handle Shift Click
     * Author: HMDUC (19/06/2023)
     * @param {*} index
     * @param {*} e
     */
    handleShiftClick(index, e) {
      e.preventDefault();
      this.indexShiftClick = index;
      this.isShiftClick = true;
      let rows = [];

      if (this.indexShiftClick > this.indexCtrlClick) {
        rows = this.assetClone.slice(
          this.indexCtrlClick + 1,
          this.indexShiftClick + 1
        );
      } else {
        rows = this.assetClone.slice(this.indexShiftClick, this.indexCtrlClick);
      }
      this.arrSelected.push(...rows);
      this.setListSelected(this.arrSelected);
    },

    /**
     * Function handle dbClick open form edit
     * Author: HMDUC (19/06/2023)
     * @param {*} data
     */
    handleOpenEdit(data) {
      this.isDbClick = true;
      this.openForm(data);
    },

    /**
     * function handle keydown event
     * Author: HMDUC (04/06/2023)
     * @param {} event
     */
    // handleKeyDown(event) {
    //   if (event.keyCode == Enum.KEY__CODE.ARROW_UP) {
    //     // Arrow up key
    //     this.moveRow(-1);
    //   } else if (event.keyCode == Enum.KEY__CODE.ARROW_DOWN) {
    //     // Arrow down key
    //     this.moveRow(1);
    //   }
    // },

    /**
     * fucntion move row up or down by keyboard
     * Author: HMDUC (04/06/2023)
     * @param {*} index
     */
    moveRow(index) {
      const newIndex = this.indexActive + index;
      if (newIndex >= 0 && newIndex < this.paginateTable.length) {
        this.indexActive = newIndex;
        this.$refs.rowItem[this.indexActive].scrollIntoView({
          block: "nearest",
          inline: "start",
        });
      }
    },

    /**
     * Function handle button Check All
     * Author: HMDUC (04/06/2023)
     */
    handleCheckAll() {
      this.isCheckAll = true;
      this.arrSelected = [...this.assetClone];
      this.setListSelected(this.arrSelected);
    },

    /**
     * Function handle button UnCheck All
     * Author: HMDUC (04/06/2023)
     */
    handleUnCheckAll() {
      this.isCheckAll = false;
      this.arrSelected.length = 0;
      this.setListSelected([]);
    },

    /**
     * function checked property to delete
     * Author: HMDUC (02/06/2023)
     * @param {*} newValue
     * @param {*} e
     */
    handleChecked(newValue, e, index) {
      e.stopPropagation();
      this.isChecked = true;
      if (e.target.checked) {
        this.arrSelected.push(newValue);
        this.indexSelected = index;
        this.setListSelected(this.arrSelected);
      }
    },
    /**
     * function uncheck , remove id selected
     * Author: HMDUC (02/06/2023)
     * @param {*} id
     */
    handleUnCheck(id) {
      this.indexSelected = null;
      const index = this.arrSelected.indexOf(id);
      this.arrSelected.splice(index, 1);
      this.setListSelected(this.arrSelected);
    },

    /**
     * Function open form when click btnEdit
     * Author: HMDUC (27/05/2023)
     */
    openForm(data) {
      this.setIsShow(true);
      this.$emit("getData", data);
      this.setFormMode(Enum.FORM__MODE.EDIT);
    },

    /**
     * Function handle btn delete on row
     * Author: HMDUC (27/05/2023)
     * @param {*} data
     */
    handleDeleteRow(data) {
      var arr = [data];
      this.$emit("showPopup", arr, "confirm");
    },

    /**
     * Function handle btn Duplicate
     * Author: HMDUC (27/05/2023)
     * @param {*} data
     */
    handleDuplicate(data) {
      this.setIsShow(true);
      this.$emit("getData", data);
      this.setFormMode(Enum.FORM__MODE.DUPLICATE);
    },

    /**
     * Function show context menu on row
     * Author: HMDUC (27/05/2023)
     */
    showContextMenu(e, data) {
      const { clientX, clientY } = e;
      this.dataSelected = data;
      this.setDataMenu(data);
      this.setShowMenu(true);
      this.setPos({ x: clientX, y: clientY });

      //add event click mouse left
      document.addEventListener("click", this.hideContextMenu);
    },
    /**
     * Function hidden context menu
     * Author: HMDUC (27/05/2023)
     */
    hideContextMenu() {
      this.setShowMenu(false);

      //add event click mouse right
      document.removeEventListener("click", this.hideContextMenu);
    },

    /**
     * Function handle btnAdd contextmenu
     */
    handleBtnAdd() {
      this.setIsShow(true);
      this.$emit("getData", null);
      this.setFormMode(Enum.FORM__MODE.ADD);
    },

    /**
     * Function handle btnEdit contextmenu
     * Author: HMDUC(28/05/2023)
     */
    handleBtnEdit(data) {
      this.setIsShow(true);
      this.$emit("getData", data);
      this.setFormMode(Enum.FORM__MODE.EDIT);
    },
    /**
     * Function handle btnDelete contextmenu
     * Author: HMDUC (28/05/2023)
     */
    handleBtnDelete(data) {
      const arrDataSelected = [];
      arrDataSelected.push(data);
      this.$emit("showPopup", arrDataSelected, "confirm");
      this.isShowPopup = true;
      this.dataSelected = data;
    },

    /**
     * Function handle btnDuplicate contextmenu
     * Author: HMDUC (28/05/2023)
     */
    handleBtnDuplicate(data) {
      this.setIsShow(true);
      this.$emit("getData", data);
      this.setFormMode(Enum.FORM__MODE.DUPLICATE);
    },
    /**
     * Function handle cancel popup
     * Author: HMDUC (28/05/2/2023)
     */
    handleCancel() {
      this.isShowPopup = false;
    },

    /**
     * function handle input page number
     * Author: HMDUC (09/06/2023)
     * @param {*} value
     * @return value input number
     */
    handleInputPage(value) {
      this.pageInput = value.replace(/[0a-zA-Z!@#$%^&*()]/g, "");

      clearTimeout(this.debounce);
      this.debounce = setTimeout(() => {
        if (this.pageInput > 0 && this.pageInput <= this.totalPage) {
          this.currentPage = this.pageInput;
        } else if (this.pageInput > this.totalPage) {
          this.currentPage = this.totalPage;
        } else if (this.pageInput === 0) {
          this.currentPage = 1;
        }
        if (this.pageInput.length > 0) {
          this.$emit("nextPage", this.currentPage);
        }
      }, 900);

      return this.pageInput;
    },

    /**
     * Function chose page size
     * @param {*} value
     * Author: HMDUC (08/06/2023)
     */
    chosePageSize(value) {
      this.$emit("pageSize", value);
    },

    /**
     * function prev firtpage
     * Author: HMDUC (08/06/2023)
     */
    firstPage() {
      if (this.currentPage > 1) {
        this.currentPage = 1;
        this.$emit("prevPage", this.currentPage);
      }
    },
    /**
     * function next lastpage
     * Author: HMDUC (08/06/2023)
     */
    lastPage() {
      if (this.currentPage < this.totalPage) {
        this.currentPage = this.totalPage;
        this.$emit("nextPage", this.currentPage);
      }
    },
    /**
     * Function handle next page
     * Author: HMDUC (27/05/2023)
     */
    nextPage() {
      if (this.currentPage < this.totalPage) {
        this.currentPage++;
        this.$emit("nextPage", this.currentPage);
      }
    },
    /**
     * Function handle prev page
     * Author: HMDUC (27/05/2023)
     */
    prevPage() {
      if (this.currentPage > 1) {
        this.currentPage--;
        this.$emit("prevPage", this.currentPage);
      }
    },
  },
};
</script>

<style>
@import "@/css/base/button.css";
@import "@/css/base/table.css";
@import "@/css/base/contextmenu.css";
@import "@/css/base/layout.css";
</style>
