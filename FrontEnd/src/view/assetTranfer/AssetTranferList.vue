<template>
  <div class="container__fluid">
    <div class="content__tranfer">
      <div class="container__header">
        <div class="select__tab">
          <m-button
            :content="this.$_MISAResources.content__button.tranferIn"
            className="btn__tab-view active__tab"
          ></m-button>
          <m-button
            :content="this.$_MISAResources.content__button.tranferOut"
            className="btn__tab-view "
          ></m-button>
          <m-button
            :content="this.$_MISAResources.content__button.receive"
            className="btn__tab-view "
          ></m-button>
        </div>
      </div>
      <div class="tab__view">
        <div class="header__tab">
          <div class="header__tab--left">
            <h4 class="title__tab">
              {{ this.$_MISAResources.title__tab.tranfer }}
            </h4>
            <m-button
              iconButton="icon__reload"
              className="btn__reload"
              :title="this.$_MISAResources.tooltip__btn.reload"
              posTooltip="top"
            ></m-button>
          </div>
          <div class="header__tab--right">
            <m-button
              className="btn__add--receipt btn__main"
              iconButton="icon__add--receipt"
              :content="this.$_MISAResources.content__button.addReceipt"
              @click="openModal"
            ></m-button>
            <m-button
              iconButton="icon__print"
              :title="this.$_MISAResources.tooltip__btn.print"
              posTooltip="top"
            ></m-button>
            <m-button
              iconButton="icon__feedBack"
              :title="this.$_MISAResources.tooltip__btn.feedBack"
              posTooltip="top"
            ></m-button>
            <m-button
              iconButton="icon__ques"
              :title="this.$_MISAResources.tooltip__btn.helper"
              posTooltip="top"
            ></m-button>
          </div>
        </div>
        <div class="tab__content">
          <div
            class="grid__master"
            :style="{ height: `calc(100% - ${heightDetail}px)` }"
          >
            <m-table-tranfer
              :columns="colReceipt"
              :dataTable="receiptList"
              :widthDefault="widthColReipt"
              :numberPage="currentPageTranfer"
              :isShowPagging="true"
              :isShowSummary="true"
              :isFixedAction="true"
              :isTableMaster="true"
              @nextPage="nextPage"
              @prevPage="prevPage"
              @pageSize="chosePageSize"
              @clickRow="handleClickRow"
            ></m-table-tranfer>
            <div
              class="ui--resiable"
              :class="[`${isResizing ? 'ui--resiable--mouse' : ''}`]"
              @mousedown="handleStartResize"
            >
              <div
                class="icon__row--resize"
                :class="[`${isResizing ? 'resize--active' : ''}`]"
              ></div>
            </div>
          </div>
          <div class="resizable-element" ref="gridDetail">
            <div
              class="grid__detail"
              :style="[{ height: heightDetail + 'px' }]"
            >
              <div class="grid__detail--header">
                <m-button
                  className="btn__infor btn__main"
                  :content="this.$_MISAResources.content__button.infor"
                ></m-button>
                <m-button
                  iconButton="icon__collapse--down"
                  className="btn__collapse-detail"
                  @click="handleCollapse"
                ></m-button>
              </div>
              <m-table-asset
                :columns="colAsset"
                :dataTable="listAssetTRanfer"
                :widthDefault="widthColAsset"
                :isShowPagging="true"
                :isTableMaster="true"
                :numberPage="currentPage"
              ></m-table-asset>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
  <m-modal :isShow="isShowModal" @closeModal="closeModal"></m-modal>
</template>

<script>
import { mapActions, mapState } from "vuex";

import MButton from "@/components/base/MButton.vue";
import MTableTranfer from "@/components/table/MTableTranfer.vue";
import MTableAssetTranfer from "@/components/table/MTableAssetTranfer";
import MModal from "@/components/base/MModal";

export default {
  name: "AssetTranferList",
  data() {
    return {
      isShowModal: false,
      isCollapse: false,
      heightDetail: 231,
      startY: 0,
      startX: 0,
      resizeHandleSize: 10,
      isResizing: false,
      colReceipt: [
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
          title: this.$_MISAResources.table.title.receiptCode,
          posLeft: true,
          key: this.$_MISAResources.table.key.receiptCode,
        },
        {
          title: this.$_MISAResources.table.title.receiptDate,
          posLeft: true,
          width: 200,
          key: this.$_MISAResources.table.key.receiptDate,
          type: "date",
        },
        {
          title: this.$_MISAResources.table.title.tranferDate,
          posLeft: true,
          width: 200,
          key: this.$_MISAResources.table.key.tranferDate,
          type: "date",
        },
        {
          title: this.$_MISAResources.table.title.orgPrice,
          posLeft: false,
          width: 200,
          key: this.$_MISAResources.table.key.orgPrice,
        },
        {
          title: this.$_MISAResources.table.title.residualPrice,
          posLeft: false,
          key: this.$_MISAResources.table.key.residualPrice,
        },
        {
          title: this.$_MISAResources.table.title.receiptNote,
          posLeft: true,
          width: 200,
          key: this.$_MISAResources.table.key.receiptNote,
        },
      ],
      colAsset: [
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
          title: this.$_MISAResources.table.title.departmentName,
          posLeft: true,
          width: 200,
          key: this.$_MISAResources.table.key.departmentName,
        },
        {
          title: this.$_MISAResources.table.title.departmentNameNew,
          posLeft: true,
          width: 200,
          key: this.$_MISAResources.table.key.departmentNameNew,
        },
        {
          title: this.$_MISAResources.table.title.cost,
          posLeft: false,
          width: 200,
          key: this.$_MISAResources.table.key.cost,
        },
        {
          title: this.$_MISAResources.table.title.residualPrice,
          posLeft: false,
          key: this.$_MISAResources.table.key.residualPrice,
        },
        {
          title: this.$_MISAResources.table.title.reason,
          posLeft: true,
          width: 200,
          key: this.$_MISAResources.table.key.reason,
        },
      ],
      widthColReipt: [45, 50, 130, 140, 140, 140, 140, 180],
      widthColAsset: [50, 100, 200, 150, 200, 140, 140],
      dataList: {
        data: [
          {
            ReceiptCode: "DC0001",
            ReceiptDate: "21/04/2023",
            TranferDate: "21/04/2023",
            ReceiptNote: "a",
            OrgPrice: 10000000000,
          },
        ],
        totalRow: 0,
      },
      currentPageTranfer: 1,
      pageSizeTranfer: 20,
    };
  },
  components: {
    "m-button": MButton,
    "m-table-tranfer": MTableTranfer,
    "m-modal": MModal,
    "m-table-asset": MTableAssetTranfer,
  },
  mounted() {
    window.addEventListener("mousemove", this.handleResize);
    window.addEventListener("mouseup", this.handleStopResize);
  },
  beforeUnmount() {
    window.removeEventListener("mousemove", this.handleResize);
    window.removeEventListener("mouseup", this.handleStopResize);
  },

  created() {
    this.getReceiptList({
      pageNumber: this.currentPageTranfer,
      pageSize: this.pageSizeTranfer,
    });
  },

  computed: {
    ...mapState("receipt", ["receiptList", "listSelectClick"]),
    ...mapState("assetTranfer", ["listAssetTRanfer"]),
  },

  methods: {
    ...mapActions("receipt", ["getReceiptList"]),
    ...mapActions("assetTranfer", ["getAssetTranferList"]),

    /**
     * Function handle click row Table Master
     * Author: HMDUC (27/07/2023)
     */
    handleClickRow(value) {
      this.getAssetTranferList({ id: value[0].ReceiptId });
    },

    /**
     * Funtion handle Collpase Form detail
     * Author: HMDUC (26/07/2023)
     */
    handleCollapse() {
      this.isCollapse = !this.isCollapse;
      if (this.isCollapse) {
        this.heightDetail = 31;
      } else {
        this.heightDetail = 231;
      }
    },

    /**
     * Function handle Start ReSize Form detail
     * Author: HMDUC (26/07/2023)
     */
    handleStartResize(event) {
      const rect = this.$refs.gridDetail.getBoundingClientRect();
      const topEdge = rect.top;
      const bottomEdge = rect.bottom;
      if (
        event.pageY <= topEdge + this.resizeHandleSize ||
        event.pageY >= bottomEdge - this.resizeHandleSize
      ) {
        this.isResizing = true;
        this.startY = event.pageY;
      }
    },

    /**
     * Funtion Resize
     *  Author: HMDUC (26/07/2023)
     */
    handleResize(event) {
      if (!this.isResizing) {
        return;
      }
      const heightDiff = event.pageY - this.startY;
      this.heightDetail -= heightDiff;
      this.startY = event.pageY;
      document.body.style.cursor = "row-resize";
    },

    /**
     * Funtion Stop Resize
     *  Author: HMDUC (26/07/2023)
     */
    handleStopResize() {
      this.isResizing = false;
      document.body.style.cursor = "";
    },
    /**
     * Funtion close modal
     * Author: HMDUC (26/07/2023)
     */
    closeModal() {
      this.isShowModal = false;
    },

    /**
     * Function open modal
     * Author: HMDUC (26/07/2023)
     */
    openModal() {
      this.isShowModal = true;
    },

    /**
     * Function nextPage Table Receipt
     * Author: HMDUC (26/07/2023)
     */
    nextPage(page) {
      this.currentPageTranfer = page;
      this.getReceiptList({
        pageNumber: this.currentPageTranfer,
        pageSize: this.pageSizeTranfer,
      });
    },

    /**
     * Function prevPage Table Receipt
     * Author: HMDUC (26/07/2023)
     */
    prevPage(page) {
      this.currentPageTranfer = page;
      this.getReceiptList({
        pageNumber: this.currentPageTranfer,
        pageSize: this.pageSizeTranfer,
      });
    },
  },
};
</script>

<style scoped>
@import "@/css/base/content.css";
@import "@/css/view/tranfer.css";

.grid__detail > .table__view {
  border-bottom: none;
  height: calc(100% - 33px);
}

.table__view {
  padding-left: 0px;
  padding-right: 0px;
  height: 100%;
  padding-bottom: unset;
  border-bottom: 1px solid #afafaf;
}
.table__view > .table__container {
  background-size: 80px;
}

/* .resizable-element {
  position: sticky;
  bottom: 0;
  width: 100%;
  z-index: 100;
}

.resizable-element::before {
  content: "";
  position: absolute;
  top: 0;
  left: 0;
  width: 100%;
  height: 7px;
  cursor: row-resize;
  border-top: solid 1px #afafaf;
  background-color: transparent;
  z-index: 999;
} */
</style>
