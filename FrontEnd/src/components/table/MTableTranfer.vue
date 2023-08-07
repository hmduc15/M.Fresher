<template>
  <div
    ref="tableRef"
    :class="['table__view', { 'table--nopaging': !isShowPagging }]"
  >
    <!-- Table Content -->
    <div
      @mousemove.stop="handleResize"
      @mouseup.capture="handleStopResize"
      class="table__container"
    >
      <div
        class="table__content table--tranfer"
        :class="[`${!isCollapsed ? 'table--tranfer-expand' : ''}`]"
      >
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
                `${column.key === 'Note' ? 'flex-1' : ''}`,
                `${column.key === 'reason' ? 'flex-1' : ''}`,
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
              {
                'table--click': isSelectedClick(data),
              },
              {
                'row--hovered':
                  this.indexRowHover === indexRow || isContextMenu(indexRow),
              },
            ]"
            @contextmenu.prevent="showContextMenu($event, data, indexRow)"
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
                `${column.key === 'Note' ? 'flex-1' : ''}`,
                `${column.key === 'reason' ? 'flex-1' : ''}`,
                `${column.type === 'date' ? 'align--center' : ''}`,
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
                <span>{{ data[column.key] }}</span>
              </template>
              <template v-else-if="column.type === 'date'">{{
                data[column.key]
              }}</template>
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
            <template v-if="column.key === 'OrgPrice'">
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
        v-if="
          (dataTable.data?.length === 0 && !isLoading) ||
          (dataTable.length === 0 && !isLoading)
        "
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
              {
                'table--click': isSelectedClick(data),
              },
            ]"
            @click="handleClickRow(data, indexRow, $event)"
            @mouseenter.stop="handleHoverStart(indexRow)"
            @mouseleave="handleHoverEnd"
          >
            <div class="fixed__ceil--item">
              <m-button
                className="btn__row btn__edit"
                iconButton="icon__edit"
                @click="handleEdit(data, $event)"
                :title="this.$_MISAResources.tooltip__btn.edit"
                posTooltip="left"
              ></m-button>
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
      <div class="loading__container" v-if="isLoading">
        <div class="row--item" v-for="index in rowLoading" :key="index">
          <m-skeleton :isLoading="true" :rows="0"></m-skeleton>
        </div>
      </div>
    </div>
    <!-- Context Menu -->
    <div
      ref="contextmenu"
      :style="{
        top: pos.y + 'px',
        left: pos.x + 'px',
        width: widthContextMenu + 'px',
        height: heightContextMenu + 'px',
      }"
      class="context__menu"
      v-show="isShowMenu"
    >
      <div class="context__button">
        <m-button
          className="btn__context"
          iconButton="icon__ctm icon__edit"
          content="Sửa"
          @click="handleEdit(dataSelected)"
        >
        </m-button>
        <m-button
          className="btn__context"
          iconButton="icon__ctm icon__delete--red"
          content="Xóa"
          @click="handleDelete(dataSelected)"
        >
        </m-button>
      </div>
    </div>
  </div>
</template>

<script>
import { mapActions, mapState } from "vuex";
import { format, formatDate } from "@/utils/format";
import Enum from "@/utils/enum";
import { request } from "@/services/request";

import MSkeleton from "../base/MSkeleton.vue";
import MTooltip from "../base/MTooltip.vue";
import MInput from "../base/MInput.vue";
import MCombobox from "../base/MCombobox.vue";
import MButton from "../base/MButton.vue";

export default {
  name: "MTableTranfer",
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
      assetTranfered: [],
      assetClone: [],
      dataSummary: {},
      posMenu: { x: 0, y: 0 },
      dataRow: null,
      dataSelected: null,
      dataDislay: null,
      isShowPopup: false,
      pageSize: 20,
      arrSelected: [],
      arrClick: [],
      isChecked: false,
      isCheckAll: false,
      isActive: true,
      indexContextMenu: -1,
      indexActive: -1,
      indexRowClick: -1,
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
      widthContextMenu: 160,
      heightContextMenu: 108,
      // data test
      dataTest: {
        data: [
          {
            ReceiptCode: "DC0001",
            ReceiptDate: "21/04/2023",
            TranferDate: "21/04/2023",
            ReceiptNote: "a",
            OrgPrice: 10000000000,
          },
          {
            ReceiptCode: "DC0001",
            ReceiptDate: "21/04/2023",
            TranferDate: "21/04/2023",
            ReceiptNote: "a",
            OrgPrice: 10000000000,
          },
          {
            ReceiptCode: "DC0001",
            ReceiptDate: "21/04/2023",
            TranferDate: "21/04/2023",
            ReceiptNote: "a",
            OrgPrice: 10000000000,
          },
          {
            ReceiptCode: "DC0001",
            ReceiptDate: "21/04/2023",
            TranferDate: "21/04/2023",
            ReceiptNote: "a",
            OrgPrice: 10000000000,
          },
          {
            ReceiptCode: "DC0001",
            ReceiptDate: "21/04/2023",
            TranferDate: "21/04/2023",
            ReceiptNote: "a",
            OrgPrice: 10000000000,
          },
          {
            ReceiptCode: "DC0001",
            ReceiptDate: "21/04/2023",
            TranferDate: "21/04/2023",
            ReceiptNote: "a",
            OrgPrice: 10000000000,
          },
          {
            ReceiptCode: "DC0001",
            ReceiptDate: "21/04/2023",
            TranferDate: "21/04/2023",
            ReceiptNote: "a",
            OrgPrice: 10000000000,
          },
          {
            ReceiptCode: "DC0001",
            ReceiptDate: "21/04/2023",
            TranferDate: "21/04/2023",
            ReceiptNote: "a",
            OrgPrice: 10000000000,
          },
          {
            ReceiptCode: "DC0001",
            ReceiptDate: "21/04/2023",
            TranferDate: "21/04/2023",
            ReceiptNote: "a",
            OrgPrice: 10000000000,
          },
          {
            ReceiptCode: "DC0001",
            ReceiptDate: "21/04/2023",
            TranferDate: "21/04/2023",
            ReceiptNote: "a",
            OrgPrice: 10000000000,
          },
          {
            ReceiptCode: "DC0001",
            ReceiptDate: "21/04/2023",
            TranferDate: "21/04/2023",
            ReceiptNote: "a",
            OrgPrice: 10000000000,
          },
          {
            ReceiptCode: "DC0001",
            ReceiptDate: "21/04/2023",
            TranferDate: "21/04/2023",
            ReceiptNote: "a",
            OrgPrice: 10000000000,
          },
          {
            ReceiptCode: "DC0001",
            ReceiptDate: "21/04/2023",
            TranferDate: "21/04/2023",
            ReceiptNote: "a",
            OrgPrice: 10000000000,
          },
          {
            ReceiptCode: "DC0001",
            ReceiptDate: "21/04/2023",
            TranferDate: "21/04/2023",
            ReceiptNote: "a",
            OrgPrice: 10000000000,
          },
          {
            ReceiptCode: "DC0001",
            ReceiptDate: "21/04/2023",
            TranferDate: "21/04/2023",
            ReceiptNote: "a",
            OrgPrice: 10000000000,
          },
          {
            ReceiptCode: "DC0001",
            ReceiptDate: "21/04/2023",
            TranferDate: "21/04/2023",
            ReceiptNote: "a",
            OrgPrice: 10000000000,
          },
          {
            ReceiptCode: "DC0001",
            ReceiptDate: "21/04/2023",
            TranferDate: "21/04/2023",
            ReceiptNote: "a",
            OrgPrice: 10000000000,
          },
          {
            ReceiptCode: "DC0001",
            ReceiptDate: "21/04/2023",
            TranferDate: "21/04/2023",
            ReceiptNote: "a",
            OrgPrice: 10000000000,
          },
          {
            ReceiptCode: "DC0001",
            ReceiptDate: "21/04/2023",
            TranferDate: "21/04/2023",
            ReceiptNote: "a",
            OrgPrice: 10000000000,
          },
          {
            ReceiptCode: "DC0001",
            ReceiptDate: "21/04/2023",
            TranferDate: "21/04/2023",
            ReceiptNote: "a",
            OrgPrice: 10000000000,
          },
        ],
        totalRow: 1,
      },
      isDeleteError: null,
      memberById: null,
    };
  },

  mounted() {
    this.$nextTick(() => {
      this.widthSkeleton = this.$refs.tableRef?.offsetWidth;
    });
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
      this.assetClone = newData.data.map((item) => {
        return {
          ...item,
          OrgPrice: this.formatValue(item.OrgPrice),
          Cost: this.formatValue(item.Cost),
          Quantity: this.formatValue(item.Quantity),
          DepreciationAmount: this.formatValue(
            Math.round(item.DepreciationAmount)
          ),
          DepreciationYear: this.formatValue(Math.round(item.DepreciationYear)),
          ResidualPrice: this.formatValue(Math.round(item.ResidualPrice)),
          ReceiptDate: this.formatDateData(item.ReceiptDate),
          TranferDate: this.formatDateData(item.TranferDate),
        };
      });
      this.dataSummary = newData.summaryData;
    },
    assetTranfered(value) {
      this.setAssetEdit(value);
    },
  },

  computed: {
    ...mapState("contextMenu", ["isShowMenu", "dataCMenu", "pos"]),
    ...mapState("asset", ["assetList", "listSelected", "reLoad"]),
    ...mapState("receipt", [
      "isLoading",
      "receiptList",
      "listSelectClick",
      "listReceiptSelected",
      "assetEdit",
    ]),
    ...mapState("displayTable", ["listDisplayed"]),
    ...mapState("sideBar", ["isCollapsed"]),
    ...mapState("modalDialog", ["isShow", "modalMode"]),
    ...mapState("loading", ["onLoading"]),

    // /**
    //  * Function check show Summary
    //  * Author: HMDUC (18/07/2023)
    //  */
    // isShowSummary() {
    //   return this.listDisplayed.some((item) => item === "Summary");
    // },

    // /**
    //  * Function check show Paging
    //  * Author: HMDUC (18/07/2023)
    //  */
    // isShowPagging() {
    //   return this.listDisplayed.some((item) => item === "Paging");
    // },

    /**
     * Function check selected row
     * Author:  HMDUC (25/06/2023)
     */
    isSelected() {
      return (data) => {
        return this.listReceiptSelected?.find(
          (receipt) => receipt.ReceiptId === data.ReceiptId
        );
      };
    },

    /**
     * Function check selected row click
     * Author: HMDUC (25/06/2023)
     */
    isSelectedClick() {
      return (data) => {
        return this.listSelectClick?.find(
          (receipt) => receipt.ReceiptId === data.ReceiptId
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
      return this.dataTable.totalRow ? this.dataTable.totalRow : 0;
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
    ...mapActions("modalDialog", ["setIsShow", "setModalMode"]),
    ...mapActions("contextMenu", ["setShowMenu", "setDataMenu", "setPos"]),
    ...mapActions("asset", ["getAssetList", "deleteAsset", "setListSelected"]),
    ...mapActions("receipt", [
      "setListSelectClick",
      "setListReceiptSelected",
      "setAssetEdit",
      "getAssetListEdit",
      "setReceiptEdit",
      "setReceiptError",
    ]),
    ...mapActions("loading", ["setLoading"]),
    ...mapActions("assetTranferChose", ["setListAssetFirst"]),

    /**
     * Function get CurrentMember By ReceiptId
     * Author: HMDUC (05/08/2023)
     */
    async getCurrentMember(id) {
      try {
        const res = await request.getCurrentMember(id);
        this.memberById = res;
        return res;
      } catch (err) {
        console.log(err);
      }
    },

    /**
     * Function get DataAsset Edit by ReceiptId
     * HMDUC (02/08/2023)
     */
    async getList(id) {
      try {
        const res = await request.getAssetEdit(id);
        var cloneJson = JSON.stringify(res);
        this.setListAssetFirst(cloneJson);
        this.$emit("getDataAsset", res);
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Function call Api check Delete
     * HMDUC (02/08/2023)
     */
    async isCheckDelete(id) {
      try {
        const res = await request.checkAccured(id);
        this.isDeleteError = res;
        return res;
      } catch (error) {
        console.log(error);
      }
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
      if (event.ctrlKey) {
        //  event ctrl + click
        this.isCtrlClick = true;
        this.indexCtrlClick = index;
        if (
          !this.arrSelected.some((item) => item.ReceiptCode == data.ReceiptCode)
        ) {
          if (!this.isCtrlClick) {
            this.arrSelected.length = 0;
          }
          this.arrSelected.push(data);
        } else {
          this.arrSelected = this.arrSelected.filter(
            (item) => item.ReceiptCode !== data.ReceiptCode
          );
        }
        console.log("d");
        this.setListReceiptSelected(this.arrSelected);
      } else if (event.shiftKey) {
        this.handleShiftClick(index, event);
      } else {
        //  normal click
        this.isCtrlClick = true;
        this.handleSelected(data);
      }
    },
    /**
     * function handle event ctrl  + click // click
     * Author: HMDUC (08/06/2023)
     * @param {*} data
     */
    handleSelected(data) {
      if (
        this.listSelectClick.some((item) => item.ReceiptId === data.ReceiptId)
      ) {
        return;
      } else {
        this.arrClick.length = 0;
        this.arrClick.push(data);
        this.setListSelectClick(this.arrClick);
        this.$emit("clickRow", this.listSelectClick);
      }
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
      this.handleEdit(data);
    },
    /**
     * Function handle button Check All
     * Author: HMDUC (04/06/2023)
     */
    handleCheckAll() {
      this.isCheckAll = true;
      this.arrSelected = [...this.assetClone];
      this.setListReceiptSelected(this.arrSelected);
    },

    /**
     * Function handle button UnCheck All
     * Author: HMDUC (04/06/2023)
     */
    handleUnCheckAll() {
      this.isCheckAll = false;
      this.arrSelected.length = 0;
      this.setListReceiptSelected([]);
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
        this.setListReceiptSelected(this.arrSelected);
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
      this.setListReceiptSelected(this.arrSelected);
    },

    /**
     * Function open form when click btnEdit
     * Author: HMDUC (27/05/2023)
     */
    async handleEdit(data) {
      this.$emit("loading", true);
      await this.getCurrentMember(data.ReceiptId);
      setTimeout(() => {
        this.$emit("getMember", this.memberById);
        this.setReceiptEdit(data);
        this.getList(data.ReceiptId);
        this.$emit("getDataReceipt", data);
        this.setIsShow(true);
        this.setModalMode(Enum.FORM__MODE.EDIT);
      }, 300);
      this.$emit("loading", false);
    },

    /**
     * Function handle btn delete on row
     * Author: HMDUC (27/05/2023)
     * @param {*} data
     */
    async handleDelete(data) {
      var arr = [data];
      await this.isCheckDelete(data.ReceiptId);
      var isError = this.isDeleteError[data.ReceiptId].length > 0;
      switch (isError) {
        case false:
          this.$emit("showPopup", arr, "confirm__receipt");
          break;
        case true:
          var assetError = this.isDeleteError[data.ReceiptId][0];
          var assetFirstError = this.isDeleteError[data.ReceiptId][0].AssetName;
          var receiptErrors = [];
          this.isDeleteError[data.ReceiptId].map((item) => {
            if (item.AssetName === assetFirstError) {
              receiptErrors.push({
                receiptCode: item.ReceiptCode,
                receiptDate: formatDate(item.ReceiptDate),
              });
            }
          });
          this.setReceiptError(receiptErrors);
          this.$emit("showPopup", assetError, "dele__error");
          break;
      }
    },

    /**
     * Function show context menu on row
     * Author: HMDUC (27/05/2023)
     */
    showContextMenu(e, data, index) {
      var temp = 0;

      var { clientX, clientY } = e;
      console.log(clientX, clientY);

      this.dataSelected = data;
      this.indexContextMenu = index;
      this.setShowMenu(true);

      // //Check position context menu
      // if (clientX > browserHeight) {
      //   clientX = clientX - this.widthContextMenu;
      // }
      // if (clientY > browserHeight) {
      //   clientY = clientY - this.heightContextMenu;
      // }

      //check collapsed sidebar
      if (!this.isCollapsed) {
        temp = -130;
      } else {
        temp = 0;
      }

      this.setPos({ x: clientX - temp - 200, y: clientY + 5 - 110 });

      //add event click mouse left
      document.addEventListener("click", this.hideContextMenu);

      // add event wheel mouse
      document.addEventListener("wheel", this.hideContextMenu);
    },
    /**
     * Function hidden context menu
     * Author: HMDUC (27/05/2023)
     */
    hideContextMenu() {
      this.setShowMenu(false);
      this.indexContextMenu = -1;

      //add event click mouse right
      document.removeEventListener("click", this.hideContextMenu);
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
.flex-1 {
  flex: 1;
}
.loading__container {
  z-index: 100;
}
.table__view {
  user-select: none;
}

/* .table__content--body,
.table__content--header,
.table__content--summary {
  width: 1304px;
} */

.table__content--summary {
  height: 37px;
  line-height: 37px;
}

.table__paging {
  position: relative;
  z-index: 99;
}
.fixed__content--header {
  user-select: none;
}

.fixed__content {
  height: calc(100% - 43px);
}

.table__content--summary {
  border-bottom: 1px solid #e9e9e9;
  z-index: 99;
}
</style>
