<template>
  <div class="container__fluid">
    <div class="content__tranfer">
      <div class="tab__view">
        <div class="header__tab">
          <div class="header__tab--left">
            <div class="tab__left--btn">
              <div class="text" v-if="this.listReceiptSelected.length === 0">
                <h4 class="title__tab">
                  {{ this.$_MISAResources.title__tab.tranfer }}
                </h4>
                <m-button
                  iconButton="icon__reload"
                  className="btn__reload"
                  :title="this.$_MISAResources.tooltip__btn.reload"
                  posTooltip="top"
                  @click="handleReload"
                ></m-button>
              </div>
              <div class="selected--checkbox" v-else>
                <div>
                  {{ this.$_MISAResources.free__text.selected }}
                  <strong class="selected__row">{{
                    this.listReceiptSelected.length
                  }}</strong>
                </div>
                <div class="clear__selected" @click="handleClearSelected">
                  {{ this.$_MISAResources.content__button.clearSelected }}
                </div>
                <m-button
                  className="btn__text btn__delete--outline btn--delete"
                  :content="this.$_MISAResources.content__button.delete"
                  type="button"
                  @click="handleDelete"
                ></m-button>
              </div>
            </div>
          </div>
          <div class="header__tab--right">
            <m-button
              className="btn__add--receipt btn__main"
              iconButton="icon__add--receipt"
              :content="this.$_MISAResources.content__button.addReceipt"
              @click="openModal"
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
              @nextPage="nextPageTranfer"
              @prevPage="prevPageTranfer"
              @pageSize="chosePageSizeTranfer"
              @clickRow="handleClickRow"
              @getDataAsset="handleDataAsset"
              @getMember="handleGetMember"
              @showPopup="handleShowPopup"
              @loading="setLoading"
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
                  :iconButton="
                    this.isCollapse
                      ? 'icon__collapse--up'
                      : 'icon__collapse--down'
                  "
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
                :isFixedAction="true"
                :numberPage="currentPageAsset"
                @nextPage="nextPageAsset"
                @prevPage="prevPageAsset"
                @pageSize="chosePageSizeAsset"
              ></m-table-asset>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
  <m-modal
    :data="dataRecieved"
    v-if="isShow"
    :newReceiptCode="newReceiptCode"
    :currentMemberEdit="memberEdit"
    @closeModal="closeModal"
    @loading="setLoading"
    @showToast="handleShowToast"
  ></m-modal>
  <m-dialog v-if="isShowPopup || isLoading || onLoading">
    <template #content v-if="isLoading || onLoading">
      <m-loading></m-loading>
    </template>
    <template #content v-else>
      <m-popup
        icon="icon__warning"
        @cancel="handleCancelPopup"
        :type="typePopup"
        :isShow="isShowPopup"
        @showToast="handleShowToast"
        :dataPopup="dataRecieved"
        :assetError="dataAsset"
        :receiptError="dataReceipt"
      ></m-popup>
    </template>
  </m-dialog>
  <m-toast
    icon="icon__check"
    :type="typeToast"
    :content="contentToast"
    :isShow="isShowToast"
  ></m-toast>
</template>

<script>
import Enum from "@/utils/enum";
import { request } from "@/services/request";
import { mapActions, mapState } from "vuex";
import { formatDate } from "@/utils/format";

import MButton from "@/components/base/MButton.vue";
import MTableTranfer from "@/components/table/MTableTranfer.vue";
import MTableAssetTranfer from "@/components/table/MTableAssetTranfer";
import MModal from "@/components/base/MModal";
import MPopup from "@/components/base/MPopup";
import MDiaglog from "@/components/base/MDiaglog";
import MToast from "@/components/base/MToast";
import MLoading from "@/components/base/MLoading";

export default {
  name: "AssetTranferList",
  data() {
    return {
      isShowPopup: false,
      typePopup: null,
      isLoading: false,
      isShowModal: false,
      isCollapse: false,
      typeToast: null,
      isShowToast: false,
      contentToast: null,
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
          title: this.$_MISAResources.table.title.oldDepartment,
          posLeft: true,
          width: 200,
          key: this.$_MISAResources.table.key.oldDepartment,
        },
        {
          title: this.$_MISAResources.table.title.departmentNameNew,
          posLeft: true,
          width: 200,
          key: this.$_MISAResources.table.key.departmentNameNew,
        },
        {
          title: this.$_MISAResources.table.title.reason,
          posLeft: true,
          width: 200,
          key: this.$_MISAResources.table.key.reason,
        },
      ],
      widthColReipt: [45, 50, 130, 140, 140, 140, 140, 180],
      widthColAsset: [50, 100, 200, 140, 140, 180, 200],
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
      receiptAssetId: null,
      currentPageTranfer: 1,
      pageSizeTranfer: 20,
      currentPageAsset: 1,
      newReceiptCode: null,
      pageSizeAsset: 20,
      dataRecieved: "",
      dataAsset: "",
      dataReceipt: "",
      memberEdit: null,
      isDeleteError: null,
    };
  },
  components: {
    "m-button": MButton,
    "m-table-tranfer": MTableTranfer,
    "m-modal": MModal,
    "m-table-asset": MTableAssetTranfer,
    "m-popup": MPopup,
    "m-dialog": MDiaglog,
    "m-toast": MToast,
    "m-loading": MLoading,
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
    this.getNewAssetCode();
    this.getReceiptList({
      pageNumber: this.currentPageTranfer,
      pageSize: this.pageSizeTranfer,
    });
  },

  computed: {
    ...mapState("receipt", [
      "receiptList",
      "listSelectClick",
      "receiptEdit",
      "listReceiptSelected",
    ]),
    ...mapState("assetTranfer", ["listAssetTRanfer"]),
    ...mapState("modalDialog", ["isShow", "modalMode"]),
    ...mapState("receipt", ["listAssetEdit", "listReceiptSelected"]),
    ...mapState("loading", ["onLoading"]),
  },

  methods: {
    ...mapActions("receipt", [
      "getReceiptList",
      "setListReceiptSelected",
      "setReceiptError",
    ]),
    ...mapActions("assetTranfer", ["getAssetTranferList"]),
    ...mapActions("modalDialog", ["setIsShow", "setModalMode"]),
    ...mapActions("assetTranferChose", [
      "setListAssetTranferChose",
      "setLoadingTableTranfer",
      "setListAssetFirst",
    ]),
    ...mapActions("assetChose", ["setListChoseSelected"]),

    /**
     * Function check Delete List Receipt
     * Author: HMDUC (06/08/2023)
     */
    async checkDeleteListReceipt(listId) {
      try {
        const res = await request.checkDeleteList(listId);
        this.isDeleteError = res;
      } catch (err) {
        console.log(err);
      }
    },

    /**
     * Function get new Code
     * Author: HMDUC (06/08/2023)
     */
    async getNewAssetCode() {
      try {
        const res = await request.get(`Receipt/NewCode`);
        this.newReceiptCode = res.toString();
      } catch (err) {
        console.log(err);
      }
    },

    async handleDelete() {
      var listId = this.listReceiptSelected.map((item) => {
        return item.ReceiptId;
      });
      await this.checkDeleteListReceipt(listId);

      switch (Object.keys(this.isDeleteError).length) {
        case 0:
          this.isShowPopup = true;
          this.typePopup = "confirm__receipt";
          this.dataRecieved = this.listReceiptSelected;
          break;
        default:
          var receiptError = [];
          var response = Object.values(this.isDeleteError)[0];
          response.map((item) => {
            receiptError.push({
              receiptCode: item.ReceiptCode,
              receiptDate: formatDate(item.ReceiptDate),
            });
          });
          this.setReceiptError(receiptError);
          this.isShowPopup = true;
          this.typePopup = "dele__error";
          this.dataAsset = response[0];
          break;
      }
    },

    /**
     * Function handle Reload
     * Author: HMDUC (06/08/2023)
     */
    handleReload() {
      this.getReceiptList({
        pageNumber: this.currentPageTranfer,
        pageSize: this.pageSizeTranfer,
      });
    },

    /**
     * Function get Current Member list
     * Author: HMDUC (06/08/2023)
     */
    handleGetMember(data) {
      this.memberEdit = data;
    },

    /**
     * Function clear selected
     * Author: HMDUC (06/08/2023)
     */
    handleClearSelected() {
      this.listReceiptSelected.length = 0;
      this.setListReceiptSelected([]);
    },

    /**
     * Function set Loading
     * Author: HMDUC (31/07/2023)
     */
    setLoading(isLoading) {
      if (isLoading) {
        this.isLoading = isLoading;
      } else {
        setTimeout(() => {
          this.isLoading = isLoading;
        }, 300);
      }
    },

    /**
     * Function handle click row Table Master
     * Author: HMDUC (27/07/2023)
     */
    handleClickRow(value) {
      this.receiptAssetId = value[0].ReceiptId;
      this.getAssetTranferList({
        id: this.receiptAssetId,
        pageNumber: this.currentPageAsset,
        pageSize: this.pageSizeAsset,
      });
    },

    /**
     * Function showPopup
     * Author: HMDUC (31/07/2023)
     */
    handleShowPopup(data, type) {
      this.typePopup = type;
      this.dataRecieved = data;
      this.isShowPopup = true;
      this.dataAsset = data;
    },

    /**
     * Function cancel and close popup
     * Author: HMDUC (31/07/2023)
     */
    handleCancelPopup() {
      this.isShowPopup = false;
      this.listReceiptSelected.length = 0;
      this.setListReceiptSelected([]);
    },

    handleShowToast(type, content) {
      this.typeToast = type;
      this.contentToast = content;
      this.isShowToast = true;
      this.isShowPopup = false;
      setTimeout(() => {
        this.isShowToast = false;
      }, 3200);
    },

    /**
     * Funtion handle Collpase Form detail
     * Author: HMDUC (26/07/2023)
     */
    handleCollapse() {
      this.isCollapse = !this.isCollapse;
      if (this.isCollapse) {
        this.heightDetail = 33;
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
      if (this.heightDetail > 533) {
        this.heightDetail = 532;
      } else if (this.heightDetail < 33) {
        this.heightDetail = 32;
      }

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
      this.setIsShow(false);
    },

    /**
     * Function open modal
     * Author: HMDUC (26/07/2023)
     */
    async openModal() {
      await this.getNewAssetCode();
      this.isLoading = true;
      setTimeout(() => {
        this.isLoading = false;
        this.setIsShow(true);
        this.setModalMode(Enum.FORM__MODE.ADD);
        this.dataRecieved = null;
      }, 300);
      this.$emit("changeMode", this.modalMode);
    },

    handleDataReceipt(data) {
      console.log(data);
      console.log(this.receiptEdit);
    },

    handleDataAsset(data) {
      var dataEdit = {
        data: data,
        totalRow: data.length,
      };
      this.setListChoseSelected(data);
      this.setListAssetTranferChose(dataEdit);
    },

    /**
     * Function nextPage Table Receipt
     * Author: HMDUC (26/07/2023)
     */
    nextPageTranfer(page) {
      this.currentPageTranfer = page;
      this.getReceiptList({
        pageNumber: this.currentPageTranfer,
        pageSize: this.pageSizeTranfer,
      });
    },

    /**
     * Function prevPage table Receipt
     * Author: HMDUC (26/07/2023)
     */
    prevPageTranfer(page) {
      this.currentPageTranfer = page;
      this.getReceiptList({
        pageNumber: this.currentPageTranfer,
        pageSize: this.pageSizeTranfer,
      });
    },

    /**
     * Function chosePage Size Table Receipt
     * Author: HMDUC  (28/07/2023)
     */
    chosePageSizeTranfer(pageSize) {
      this.pageSizeTranfer = pageSize;
      this.getReceiptList({
        pageNumber: this.currentPageTranfer,
        pageSize: this.pageSizeTranfer,
      });
    },

    /**
     * Function nextPage Table Asset Receipt
     * Author: HMDUC  (28/07/2023)
     */
    nextPageAsset(page) {
      this.currentPageAsset = page;
      this.getAssetTranferList({
        id: this.receiptAssetId,
        pageNumber: this.currentPageAsset,
        pageSize: this.pageSizeAsset,
      });
    },

    /**
     * Function nextPage Table Asset Receipt
     * Author: HMDUC  (28/07/2023)
     */
    prevPageAsset(page) {
      this.currentPageAsset = page;
      this.getAssetTranferList({
        id: this.receiptAssetId,
        pageNumber: this.currentPageAsset,
        pageSize: this.pageSizeAsset,
      });
    },

    /**
     * Function chosePage Size  Table Asset Receipt
     * Author: HMDUC  (28/07/2023)
     */
    chosePageSizeAsset(pageSize) {
      this.pageSizeAsset = pageSize;
      this.getAssetTranferList({
        id: this.receiptAssetId,
        pageNumber: this.currentPageAsset,
        pageSize: this.pageSizeAsset,
      });
    },
  },
};
</script>

<style scoped>
@import "@/css/base/content.css";
@import "@/css/view/tranfer.css";

.text {
  display: flex;
  justify-content: center;
  align-items: center;
  gap: 0 5px;
}

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
