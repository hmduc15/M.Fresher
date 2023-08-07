<template>
  <div
    ref="tableRef"
    :class="['table__view', { 'table--nopaging': !isShowPagging }]"
  >
    <div class="input--wrapper">
      <m-input
        type="text"
        name="inputSearch"
        id=""
        :placeHolder="this.$_MISAResources.placeholder.SearchInput"
        className="input__text input__text--icon input--default"
        :required="false"
        :isLabel="false"
        :requiredNot="true"
        v-model="searchText"
        @keydown.enter="handleSearch(searchText)"
        @input="clearSearch(searchText)"
      >
      </m-input>
      <div class="icon__search--sm btn__search"></div>
    </div>
    <!-- Table Content -->
    <div
      @mousemove.stop="handleResize"
      @mouseup.capture="handleStopResize"
      class="table__container"
    >
      <div class="table__content table--tranfer">
        <!-- Table Header -->
        <div
          class="table__content--header"
          :class="[
            `${isShowModal ? 'table--modal' : ''}`,
            `${isTableMaster ? 'table--master' : ''}`,
            `${isTableChose ? 'table--fw' : ''}`,
          ]"
        >
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
                `${column.key === 'ReceiptNote' ? 'flex-1' : ''}`,
                `${column.key === 'Reason' ? 'flex-1' : ''}`,
                `${column.type === 'date' ? 'align--center' : ''}`,
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
        <!-- Table Body -->
        <div
          :class="[
            `${isShowModal ? 'table--modal' : ''}`,
            `${isTableMaster ? 'table--master' : ''}`,
            `${isTableChose ? 'table--fw' : ''}`,
          ]"
          class="table__content--body"
          ref="tBody"
          @scroll="hanldeScroll($event)"
        >
          <div
            v-for="(data, indexRow) in assetClone"
            :key="indexRow"
            class="row--item"
            ref="rowItem"
            :row-index="indexRow"
            :class="[
              'table__body--row',
              {
                'table--selected': isSelected(data),
              },
              // {
              //   'table--click': isSelectedClick(data),
              // },
              {
                'row--hovered':
                  this.indexRowHover === indexRow || isContextMenu(indexRow),
              },
            ]"
            @contextmenu.prevent="showContextMenu($event, data, indexRow)"
            @click="handleClickRow(data, indexRow, $event)"
            @mouseenter.stop="handleHoverStart(indexRow)"
            @mouseleave.stop="handleHoverEnd"
          >
            <div
              class="ceil--item"
              v-for="(column, index) in columns"
              :data-index="index"
              :key="index"
              :class="[
                `${column.posLeft ? 'align--left' : 'align--right'}`,
                `${column.action ? ' align--center' : ''}`,
                `${column.checkbox ? 'align--center' : ''}`,
                `${column.key === 'order' ? 'align--center' : ''}`,
                `${column.key === 'ReceiptNote' ? 'flex-1' : ''}`,
                `${column.key === 'Reason' ? 'flex-1' : ''}`,
                `${column.type === 'date' ? 'align--center' : ''}`,
                `${
                  column.key === 'DepartmentReceipt'
                    ? 'd-flex jus--center ali--item'
                    : ''
                }`,
              ]"
              :style="[`width: ${widthDefault[index]}px`]"
              :title="data[column.key]"
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
              <template v-else-if="column.key === 'OrgPrice'">
                <span>{{ this.formatValue(data[column.key]) }}</span>
              </template>
              <template v-else-if="column.type === 'date'">{{
                this.formatDateData(data[column.key])
              }}</template>
              <template v-else-if="column.key === 'DepartmentReceipt'">
                <m-combobox
                  :ref="`departmentReciept${indexRow}`"
                  :placeholder="this.$_MISAResources.placeholder.DepartmentCode"
                  name="DepartmentReciept"
                  :isFilter="true"
                  :required="true"
                  :isLabel="false"
                  :listOptions="listDepartment"
                  v-model="data[column.key]"
                  className="select--table"
                  @change="changeDepartment"
                  @click="handlClickCbk(data, indexRow)"
                ></m-combobox>
              </template>
              <template v-else-if="column.key === 'Reason'">
                <m-input
                  :ref="`reason${indexRow}`"
                  v-model="data[column.key]"
                  type="text"
                  className="input__text input--default input--ceil"
                  :required="false"
                  :requiredNot="true"
                  :isLabel="false"
                  @input="handleInput($event)"
                  @click.stop="getIdRow(data)"
                ></m-input>
              </template>
              <template v-else>
                <span>{{ data[column.key] }}</span>
              </template>
            </div>
          </div>
        </div>
        <!-- Table Summary -->
        <div
          :class="[
            `${isShowModal ? 'table--modal' : ''}`,
            `${isTableMaster ? 'table--master' : ''}`,
            `${isTableChose ? 'table--fw' : ''}`,
          ]"
          class="table__content--summary"
          v-if="isShowSummary"
          ref="summaryTable"
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
              `${column.key === 'reason' ? 'flex-1' : ''}`,
              `${column.key === 'ReceiptNote' ? 'flex-1' : ''}`,
            ]"
            :style="[`width: ${widthDefault[index]}px`]"
          >
            <template v-if="column.key === 'Quantity'">
              <span>{{
                this.formatValue(this.dataSummary.total_quantity)
              }}</span>
            </template>
            <template v-if="column.key === 'Cost'">
              <span>{{
                this.dataSummary.total_cost ? this.dataSummary.total_cost : 0
              }}</span>
            </template>
            <template v-if="column.key === 'DepreciationAmount'">
              <span>{{
                this.formatValue(this.dataSummary.total_depreciation)
              }}</span>
            </template>
            <template v-if="column.key === 'ResidualPrice'">
              <span>{{
                this.dataSummary.total_residual_price
                  ? this.dataSummary.total_residual_price
                  : 0
              }}</span>
            </template>
          </div>
        </div>
      </div>
      <!-- Table Pagging -->
      <div class="table__paging" v-if="isShowPagging">
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
                :className="[
                  'btn__prev',
                  { 'btn--notallowed': this.currentPage === 1 },
                  { 'btn--hover': this.currentPage > 1 },
                ]"
                iconButton="icon__prev"
                @click="prevPage"
              ></m-button>
            </template>
          </m-tooltip>
          <div class="page__container">
            <m-input
              type="text"
              v-model="currentPage"
              :className="['input__paging']"
              @input="currentPage = this.handleInputPage(currentPage)"
            ></m-input>
          </div>
          <m-tooltip :content="this.$_MISAResources.tooltip__btn.nextPage">
            <template #child>
              <m-button
                :className="[
                  'btn__next',
                  { 'btn--notallowed': this.currentPage === this.totalPage },
                  { 'btn--hover': this.currentPage < this.totalPage },
                ]"
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
      <!-- Table Empty -->
      <div
        class="empty__data empty__data--sm icon--empty"
        v-if="dataTable.data?.length === 0 && !isLoadingTableTranfer"
      ></div>
      <!-- Table Fixed Content -->
      <div class="fixed__content" v-if="isFixedAction">
        <!-- Header -->
        <div class="fixed__content--header">
          <span>{{ this.$_MISAResources.table.title.action }}</span>
        </div>
        <!-- Body -->
        <div
          class="fixed__content--body"
          ref="fBody"
          @scroll="hanldeScroll($event)"
        >
          <div
            class="fixed__row--item"
            v-for="(data, indexRow) in assetClone"
            :key="indexRow"
            :row-index="indexRow"
            :class="[
              {
                'table--selected': isSelected(data),
                'row--hovered':
                  this.indexRowHover === indexRow || isContextMenu(indexRow),
              },
            ]"
            @click="handleClickRow(data, indexRow, $event)"
            @mouseenter.s="handleHoverStart(indexRow)"
            @mouseleave="handleHoverEnd"
          >
            <div class="fixed__ceil--item">
              <m-button
                className="btn__row btn__delete--table"
                iconButton="icon__delete--red"
                :title="this.$_MISAResources.tooltip__btn.delete"
                posTooltip="top"
                @click="handleDelete(data)"
              ></m-button>
            </div>
          </div>
        </div>
        <!-- Summary -->
        <div
          class="fixed__content--summary"
          v-if="isShowSummary"
          ref="summaryFixed"
          name="summary"
        ></div>
      </div>
      <!-- Loading -->
      <div class="loading__container" v-if="isLoadingTableTranfer">
        <div class="row--item" v-for="index in rowLoading" :key="index">
          <m-skeleton :isLoading="true" :rows="0"></m-skeleton>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { request } from "@/services/request";

import { mapActions, mapState } from "vuex";
import { format, formatDate } from "@/utils/format";
import Enum from "@/utils/enum";
import MSkeleton from "../base/MSkeleton.vue";
import MTooltip from "../base/MTooltip.vue";
import MInput from "../base/MInput.vue";
import MCombobox from "../base/MCombobox.vue";
import MButton from "../base/MButton.vue";

export default {
  name: "MTableAssetChose",
  props: [
    "columns",
    "state",
    "dataTable",
    "numberPage",
    "widthDefault",
    "isShowSummary",
    "isShowPagging",
    "isFixedAction",
    "isShowModal",
    "isTableMaster",
    "isTableChose",
    "idReceipt",
  ],

  components: {
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
      pageSize: 20,
      arrSelected: [],
      isChecked: false,
      isCheckAll: false,
      isActive: true,
      indexContextMenu: -1,
      indexActive: -1,
      indexRowClick: -1,
      isToggle: false,
      indexShiftClick: -1,
      indexCtrlClick: -1,
      indexRowHover: -1,
      isShiftClick: false,
      isCtrlClick: false,
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
      // widthDefault: [45, 50, 110, 200, 180, 170, 80, 140, 110, 130, 110],
      indexResize: null,
      startX: 0,
      startWidth: 0,
      minWidth: 100,
      debounce: null,
      currentPage: this.numberPage,
      newAssetCode: null,
      widthContextMenu: 200,
      heightContextMenu: 146,
      listDepartment: null,
      newDepartment: null,
      selectedOption: null,
      indexCbk: null,
      rowChange: null,
      totalCost: 0,
      totalResidualPrice: 0,
      isDeleteError: null,
      searchText: null,
      arrBeforeSearch: null,
    };
  },

  mounted() {
    this.$nextTick(() => {
      this.widthSkeleton = this.$refs.tableRef?.offsetWidth;
    });
  },

  created() {
    this.getDepartmentList();
  },

  watch: {
    numberPage() {
      this.currentPage = this.numberPage;
    },

    currentPage(newPage) {
      this.pageInput = newPage;
    },

    listSelected(newValue) {
      if (newValue.length === 0) {
        this.arrSelected.length = 0;
      } else {
        this.arrSelected = newValue;
      }
    },

    reLoad(newValue) {
      if (newValue) {
        this.currentPage = 1;
      }
    },

    dataTable(newData) {
      this.totalCost = 0;
      this.totalResidualPrice = 0;
      this.assetClone = newData.data.map((item) => {
        if (this.modalMode === Enum.FORM__MODE.ADD) {
          this.totalCost += parseInt(item.Cost.replace(/\./g, ""));
          this.totalResidualPrice += parseInt(
            item.ResidualPrice.replace(/\./g, "")
          );
          return {
            ...item,
            Quantity: this.formatValue(item.Quantity),
            DepreciationAmount: this.formatValue(
              Math.round(item.DepreciationAmount)
            ),
            DepreciationYear: this.formatValue(
              Math.round(item.DepreciationYear)
            ),
          };
        } else {
          this.totalCost += item.Cost;
          this.totalResidualPrice += Math.floor(item.ResidualPrice);
          return {
            ...item,
            Cost: this.formatValue(item.Cost),
            ResidualPrice: this.formatValue(Math.floor(item.ResidualPrice)),
            Quantity: this.formatValue(item.Quantity),
            DepreciationAmount: this.formatValue(
              Math.round(item.DepreciationAmount)
            ),
            DepreciationYear: this.formatValue(
              Math.round(item.DepreciationYear)
            ),
          };
        }
      });
      this.dataSummary = {
        total_cost: this.formatValue(this.totalCost),
        total_residual_price: this.formatValue(this.totalResidualPrice),
      };
      let jsonBeforeSearch = JSON.stringify(this.assetClone);
      this.arrBeforeSearch = JSON.parse(jsonBeforeSearch);
    },
  },

  computed: {
    ...mapState("assetTranferChose", [
      "listAssetTranferChose",
      "isLoadingTableTranfer",
      "listSelected",
    ]),
    ...mapState("modalDialog", ["isShow", "modalMode"]),
    ...mapState("receipt", [
      "listAssetEdit",
      "receiptList",
      "receiptEdit",
      "listAssetDelete",
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
     * Function check isContextMenu
     * Author:  HMDUC (25/06/2023)
     */
    isContextMenu() {
      return (index) => {
        return this.indexContextMenu === index;
      };
    },

    /**
     * Function return totalrecord
     * Author: HMDUC(26/05/2023)
     */
    paginateTable() {
      return this.assetList.data;
    },

    /**
     * Function return total record
     * Author: HMDUC(26/05/2023)
     */
    totalRecored() {
      return this.assetClone.length ? this.assetClone.length : 0;
    },
    /**
     * Function return total page
     * Author: HMDUC(26/05/2023)
     */
    totalPage() {
      return this.dataTable.totalRow
        ? Math.ceil(this.dataTable.totalRow / this.pageSize)
        : 1;
    },
  },

  methods: {
    ...mapActions("assetTranfer", ["getAssetTranferList"]),
    ...mapActions("assetTranferChose", [
      "setListAssetTranferChose",
      "setLoadingTableTranfer",
      "setListSelected",
    ]),
    ...mapActions("assetChose", ["setListChoseSelected"]),
    ...mapActions("receipt", ["setReceiptError", "setListAssetDelete"]),

    /**
     * Function call Api check before Delete
     * Author: HMDUC(06/08/2023)
     */
    async checkDelete(assetId, receiptId) {
      try {
        const res = await request.checkDeleteAssetReceipt(assetId, receiptId);
        this.isDeleteError = res;
        return res;
      } catch (err) {
        console.log(err);
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
          id: item.DepartmentId,
          value: item.DepartmentCode,
          label: item.DepartmentName,
        }));
        this.listDepartment = mapData;
      } catch (err) {
        this.isShowToast = true;
        this.typeToast = "notice";
        this.contentToast = this.$_MISAResources.toast__content.ErrorServer;
      }
    },

    /**
     * Function handleSearch Asset
     * Author: HMDUC (01/06/2023)
     */
    handleSearch(value) {
      let totalCost = 0;
      let totalResidualPrice = 0;
      if (this.arrBeforeSearch.length > 0) {
        const textLower = value.toLowerCase();
        var result = this.arrBeforeSearch.filter((item) => {
          const assetCodeLower = item.AssetCode.toLowerCase();
          const assetNameLower = item.AssetName.toLowerCase();
          return (
            assetCodeLower.includes(textLower) ||
            assetNameLower.includes(textLower)
          );
        });

        result.map((item) => {
          if (this.modalMode === Enum.FORM__MODE.ADD) {
            totalCost += parseInt(item.Cost.replace(/\./g, ""));
            totalResidualPrice += parseInt(
              item.ResidualPrice.replace(/\./g, "")
            );
          } else {
            totalCost += parseInt(item.Cost.replace(/\./g, ""));
            totalResidualPrice += parseInt(
              item.ResidualPrice.replace(/\./g, "")
            );
          }
        });
        this.dataSummary = {
          total_cost: this.formatValue(totalCost),
          total_residual_price: this.formatValue(totalResidualPrice),
        };
        this.assetClone = result;
      }
    },

    /**
     * Function clear search
     * Author: HMDUC (01/06/2023)
     */
    clearSearch() {
      if (this.searchText.length === 0) {
        setTimeout(() => {
          this.assetClone = this.arrBeforeSearch;
          this.dataSummary = {
            total_cost: this.formatValue(this.totalCost),
            total_residual_price: this.formatValue(this.totalResidualPrice),
          };
        }, 500);
      }
    },

    /**
     * function handle select option
     * Author: HMDUC (01/06/2023)
     * @param {*} obj contain id and name department
     *
     */
    changeDepartment(obj) {
      this.selectedOption = obj;
      const department = this.findOptionById(
        this.listDepartment,
        this.selectedOption
      );
      this.newDepartment = department;
      var idChange = this.assetClone.find((item) => {
        return item.DepartmentReceipt === this.newDepartment.value;
      });
      this.$emit("changed", this.newDepartment, idChange.AssetId);
      this.handleFocusControl(this.indexCbk, false);
    },

    handlClickCbk(data, index) {
      this.rowChange = data;
      this.indexCbk = index;
      this.isToggle = true;
      //     this.handleFocusControl(index, true);
    },

    /**
     * Function control focus
     * Author: HMDUC (02/08/2023)
     */
    handleFocusControl(index, isFocus) {
      if (isFocus) {
        const elCombobox = this.$refs[`departmentReciept${index}`];
        if (elCombobox) {
          this.$nextTick(() => {
            elCombobox[0].isBorder = true;
          });
        }
      } else {
        const elCombobox = this.$refs[`departmentReciept${index}`];
        if (elCombobox) {
          this.$nextTick(() => {
            elCombobox[0].isBorder = false;
          });
        }
      }
    },

    handleInput(e) {
      this.$emit("inputReason", e.target.value, this.rowChange.AssetId);
    },

    getIdRow(data) {
      this.rowChange = data;
    },

    /**
     * function find option selected by id when select departmentID or  categoryID
     * Author: HMDUC (01/06/2023)
     * @param {*} arr
     * @param {*} opt
     * @return Id: BP001 -> Phòng kỹ thuật || TS001 -> Máy tính xách tay
     */
    findOptionById(arr, opt) {
      return arr.find((item) => {
        return item.value == opt;
      });
    },

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

    // /**
    //  * Function handle resize event
    //  * @param {} event
    //  * Author: HMDUC (20/06/2023)
    //  */
    // handleResize(event) {
    //   if (this.indexResize != null) {
    //     const width = event.pageX - this.startX;
    //     this.widthDefault[this.indexResize] = this.startWidth + width;
    //     if (this.widthDefault[this.indexResize] < this.minWidth) {
    //       this.widthDefault[this.indexResize] = this.minWidth;
    //     }
    //   }
    // },

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
      if (this.isFixedAction) {
        const tBody = this.$refs.tBody;
        const fBody = this.$refs.fBody;

        if (e.target == tBody) {
          fBody.scrollTop = tBody.scrollTop;
        } else {
          tBody.scrollTop = fBody.scrollTop;
        }
      }
    },

    /**
     * Function format date
     * Author: HMDUC (26/07/2023)
     */
    formatDateData(value) {
      let date = format.formatDate(value);
      return date;
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
    formatDenary(value) {
      let float = format.formatDenary(value);
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
      this.isToggle = !this.isToggle;
      if (this.isToggle) {
        const elCombobox = this.$refs[`departmentReciept${index}`];
        const elInput = this.$refs[`reason${index}`];
        if (elCombobox) {
          this.$nextTick(() => {
            elCombobox[0].isBorder = true;
            elInput[0].isBorder = true;
          });
        }
      } else {
        const elInput = this.$refs[`reason${index}`];
        const elCombobox = this.$refs[`departmentReciept${index}`];
        if (elCombobox) {
          this.$nextTick(() => {
            elCombobox[0].isBorder = false;
            elInput[0].isBorder = false;
          });
        }
      }

      if (event.ctrlKey) {
        //  event ctrl + click
        this.isCtrlClick = true;
        this.indexCtrlClick = index;
        if (!this.arrSelected.some((item) => item.AssetId == data.AssetId)) {
          if (!this.isCtrlClick) {
            this.arrSelected.length = 0;
          }
          this.arrSelected.push(data);
        } else {
          this.arrSelected = this.arrSelected.filter(
            (item) => item.AssetId !== data.AssetId
          );
        }
        this.setListSelected(this.arrSelected);
      } else if (event.shiftKey) {
        //event shift + click
        this.handleShiftClick(index, event);
      } else {
        //normal click
        this.isCtrlClick = false;
        this.handleSelected(data, index, event);
      }
    },
    /**
     * function handle event ctrl  + click // click
     * Author: HMDUC (08/06/2023)
     * @param {*} data
     */
    handleSelected(data, index) {
      this.indexCtrlClick = index;
      if (!this.arrSelected.some((item) => item.AssetCode == data.AssetCode)) {
        this.arrSelected.push(data);
      } else {
        this.arrSelected = this.arrSelected.filter(
          (item) => item.AssetCode !== data.AssetCode
        );
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
    handleEdit(data) {
      this.$emit("loading", true);

      setTimeout(() => {
        this.setIsShow(true);
        this.$emit("getData", data);
        this.setFormMode(Enum.FORM__MODE.EDIT);
      }, 300);

      this.$emit("loading", false);
    },

    /**
     * Function handle btn delete on row
     * Author: HMDUC (27/05/2023)
     * @param {*} data
     */
    async handleDelete(data) {
      var arrAssetId = this.listAssetTranferChose.data.map((item) => {
        return item.AssetId;
      });
      var index = arrAssetId.indexOf(data.AssetId);
      if (this.modalMode === Enum.FORM__MODE.ADD) {
        this.assetClone.splice(index, 1);
        this.listAssetDelete.push(data);
      } else {
        await this.checkDelete(data.AssetId, this.receiptEdit.ReceiptId);
        var response = this.isDeleteError;
        switch (Object.keys(response).length) {
          case 0:
            this.listAssetDelete.push(data);
            this.assetClone.splice(index, 1);
            break;
          default:
            var receiptError = [];
            response[data.AssetCode].map((item) => {
              receiptError.push({
                receiptCode: item.ReceiptCode,
                receiptDate: formatDate(item.ReceiptDate),
              });
            });
            this.setReceiptError(receiptError);
            this.$emit(
              "showPopup",
              response[data.AssetCode][0],
              "asset__dele-err"
            );
        }
      }
      this.assetClone.forEach((item) => {
        (item.Cost = parseInt(item.Cost.replace(/\./g, ""))),
          (item.ResidualPrice = parseInt(
            item.ResidualPrice.replace(/\./g, "")
          ));
      });
      var dataUpdate = {
        data: this.assetClone,
        totalRow: this.assetClone.length,
      };
      this.setListAssetTranferChose(dataUpdate);
      this.setListChoseSelected(this.assetClone);
    },

    /**
     * function handle input page number
     * Author: HMDUC (09/06/2023)
     * @param {*} value
     * @return value input number
     */
    handleInputPage(value) {
      this.pageInput = value.replace(/[a-zA-Z!@#$%^&*()]/g, "");
      clearTimeout(this.debounce);
      this.debounce = setTimeout(() => {
        if (this.pageInput > 0 && this.pageInput <= this.totalPage) {
          this.currentPage = this.pageInput;
        } else if (this.pageInput > this.totalPage) {
          this.currentPage = this.totalPage;
        } else if (this.pageInput == 0) {
          this.currentPage = 1;
        }
        if (this.pageInput.length > 0) {
          this.$emit("nextPage", this.currentPage);
        }
      }, 1000);

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

<style scoped>
@import "@/css/base/button.css";
@import "@/css/base/table.css";
@import "@/css/base/contextmenu.css";
@import "@/css/base/layout.css";
@import "@/css/view/table.css";

.table__view {
  display: flex;
  flex-direction: column;
  position: relative;
}
.table__container {
  flex: 1;
}

.wrap--left {
  display: flex;
  align-items: center;
  gap: 0 10px;
}

.input--wrapper {
  z-index: 99;
  position: absolute;
  top: -44px;
  left: 200px;
}
.table__content--header {
  z-index: 60;
}

.flex-1 {
  flex: 1;
}

.table__view {
  user-select: none;
}

.table__content--summary {
  height: 37px;
  line-height: 37px;
}

.fixed__content--header {
  user-select: none;
}

.fixed__content {
  height: calc(100% - 43px);
  width: 90px;
  z-index: 82;
}

.table__content--summary {
  border-bottom: 1px solid #e9e9e9;
}

.empty__data--sm {
  background-size: 135px;
}
</style>
