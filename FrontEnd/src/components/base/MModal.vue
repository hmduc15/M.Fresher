<template>
  <div
    class="modal"
    v-if="isShow"
    v-esc="!isShowFormAsset ? handleCloseModal : ''"
  >
    <div class="modal__content">
      <div class="modal__header">
        <div class="modal--title">
          <h5>{{ this.$_MISAResources.form__title.AddReceipt }}</h5>
        </div>
        <m-button
          iconButton="icon__close-v2"
          className="btn__close-v2"
          :title="this.$_MISAResources.tooltip__btn.close"
          @click="handleCloseModal"
        ></m-button>
      </div>
      <div class="modal--body">
        <div class="body__content">
          <div id="title">
            {{ this.$_MISAResources.form__title.ComonInfor }}
          </div>
          <div class="card">
            <div class="card__body">
              <div class="row">
                <div class="col-2">
                  <m-input
                    v-model="receipt.ReceiptCode"
                    type="text"
                    name="ReceiptCode"
                    className="input__text input__full"
                    :required="true"
                    :isLabel="true"
                  ></m-input>
                </div>
                <div class="col-2">
                  <m-input
                    v-model="receipt.ReceiptDate"
                    type="date"
                    name="ReceiptDate"
                    className="input__text input__full"
                    :required="true"
                    :isLabel="true"
                  ></m-input>
                </div>
                <div class="col-2">
                  <m-input
                    v-model="receipt.TranferDate"
                    type="date"
                    name="TranferDate"
                    className="input__text input__full"
                    :required="true"
                    :isLabel="true"
                  ></m-input>
                </div>
                <div class="col-6">
                  <m-input
                    v-model="receipt.Note"
                    type="text"
                    name="Note"
                    className="input__text input__full"
                    :required="true"
                    :isLabel="true"
                  ></m-input>
                </div>
              </div>
            </div>
          </div>
          <div class="detail__action">
            <div id="title" class="mg--16">
              {{ this.$_MISAResources.form__title.TranferInfor }}
            </div>
            <div class="detail__button">
              <m-button
                :content="this.$_MISAResources.content__button.choseAsset"
                className="btn__chose btn__text"
                iconButton="icon__chose"
                @click="openFormChose"
              ></m-button>
            </div>
          </div>
          <div class="body__table">
            <m-table-asset-chose
              :columns="colAssetTranfer"
              :widthDefault="widthColAssetTranfer"
              :dataTable="listAssetTranferChose"
              :isFixedAction="true"
              :isShowPagging="true"
              :isShowSummary="true"
              :isShowModal="isShow"
            ></m-table-asset-chose>
          </div>
        </div>
      </div>
      <div class="modal--footer">
        <m-button
          type="button"
          :content="this.$_MISAResources.content__button.cancelForm"
          className="btn__sub btn--form "
          @click="handleCloseModal"
        ></m-button>
        <m-button
          type="button"
          ref="lastBtn"
          :content="this.$_MISAResources.content__button.save"
          className="btn__main btn__submit"
        ></m-button>
      </div>
    </div>
    <!-- Form Chose Asset -->
    <m-dialog v-if="isShowPopup || isShowFormAsset">
      <template #content v-if="isShowFormAsset">
        <div class="form__chose" v-esc="closeFormChoseAsset">
          <div class="form__chose--header">
            <div class="modal--title">
              <h5>{{ this.$_MISAResources.form__title.AddReceiptAsset }}</h5>
            </div>
            <m-button
              iconButton="icon__close-v2"
              className="btn__close-v2"
              :title="this.$_MISAResources.tooltip__btn.close"
              @click="closeFormChoseAsset"
            ></m-button>
          </div>
          <div class="form__chose--body">
            <m-table-chose
              :columns="colAssetChose"
              :widthDefault="widthColAssetChose"
              :dataTable="listAssetChose"
              :numberPage="currentPageChose"
              :isShowPagging="true"
              :isShowSummary="true"
              :isTableChose="true"
              @nextPage="nextPage"
              @prevPage="prevPage"
              @pageSize="chosePageSize"
            ></m-table-chose>
            <div class="border--bottom"></div>
            <div class="infor__new">
              <div class="row">
                <div class="col-3">
                  <m-input
                    type="text"
                    name="DepartmentCodeNew"
                    className="input__text input__full"
                    :required="true"
                    :isLabel="true"
                  ></m-input>
                </div>
                <div class="col-3">
                  <m-input
                    type="text"
                    name="UserNew"
                    className="input__text input__full"
                    :required="true"
                    :isLabel="true"
                  ></m-input>
                </div>
                <div class="col-6">
                  <m-input
                    v-model="receipt.Note"
                    type="text"
                    name="Note"
                    className="input__text input__full"
                    :required="true"
                    :isLabel="true"
                  ></m-input>
                </div>
              </div>
            </div>
          </div>
          <div class="form__chose--footer">
            <m-button
              type="button"
              :content="this.$_MISAResources.content__button.cancelForm"
              className="btn__sub btn--form-chose btn--form "
              @click="closeFormChoseAsset"
            ></m-button>
            <m-button
              type="button"
              ref="lastBtn"
              :content="this.$_MISAResources.content__button.save"
              className="btn__main btn__submit  "
              @click="confirmAssetChose"
            ></m-button>
          </div>
        </div>
      </template>
      <template #content v-else>
        <m-popup
          type="warning__modal"
          :isShow="isShowPopup"
          icon="icon__warning"
          @cancel="handleCancel"
        ></m-popup>
      </template>
    </m-dialog>
  </div>
</template>

<script>
import MButton from "@/components/base/MButton";
import MInput from "@/components/base/MInput";
import MTableChose from "@/components/table/MTableChose";
import MPopup from "@/components/base/MPopup";
import MDiaglog from "@/components/base/MDiaglog";
import MTableAssetChose from "@/components/table/MTableAssetChose";
import { mapActions, mapState } from "vuex";

export default {
  name: "MModal",
  props: ["isShow"],
  components: {
    "m-button": MButton,
    "m-input": MInput,
    "m-table-chose": MTableChose,
    "m-table-asset-chose": MTableAssetChose,
    "m-popup": MPopup,
    "m-dialog": MDiaglog,
  },

  computed: {
    ...mapState("assetTranferChose", [
      "listAssetTranferChose",
      "isLoadingTableTranfer",
    ]),
    ...mapState("assetChose", ["listAssetChose", "listAssetChoseSelected"]),
  },

  created() {
    this.dataList = {
      data: [],
      totalRow: 0,
    };
    this.setListAssetTranferChose(this.dataList);
  },

  data() {
    return {
      widthColAssetTranfer: [45, 50, 130, 170, 188, 188, 200, 188, 188],
      widthColAssetChose: [45, 55, 130, 200, 170, 160, 180, 110],
      isShowPopup: false,
      isShowFormAsset: false,
      arrListChoseTranfer: [],
      arrSeleted: [],
      dataList: {},
      arrListId: [],
      ids: [],
      currentPageChose: 1,
      pageSizeChose: 20,
      colAssetTranfer: [
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
          title: this.$_MISAResources.table.title.departmentName,
          posLeft: true,
          width: 200,
          key: this.$_MISAResources.table.key.departmentName,
        },
        {
          title: this.$_MISAResources.table.title.userAsset,
          posLeft: true,
          width: 200,
          key: this.$_MISAResources.table.key.userAsset,
        },
        {
          title: this.$_MISAResources.table.title.departmentNameNew,
          posLeft: true,
          width: 200,
          key: this.$_MISAResources.table.key.departmentNameNew,
        },
        {
          title: this.$_MISAResources.table.title.newUserAsset,
          posLeft: true,
          width: 200,
          key: this.$_MISAResources.table.key.newUserAsset,
        },
        {
          title: this.$_MISAResources.table.title.reason,
          posLeft: true,
          width: 200,
          key: this.$_MISAResources.table.key.reason,
        },
      ],
      colAssetChose: [
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
          title: this.$_MISAResources.table.title.departmentName,
          posLeft: true,
          width: 200,
          key: this.$_MISAResources.table.key.departmentName,
        },
        {
          title: this.$_MISAResources.table.title.cost,
          posLeft: false,
          key: this.$_MISAResources.table.key.cost,
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
      receipt: {
        ReceiptCode: "",
        ReceiptDate: "",
        TranferDate: "",
        Note: "",
      },
    };
  },

  methods: {
    ...mapActions("assetTranferChose", [
      "setListAssetTranferChose",
      "setLoadingTableTranfer",
    ]),
    ...mapActions("assetChose", ["getListAssetChose", "setListChoseSelected"]),

    /**
     * Function confirm Asset Tranfer form Chose
     * Author: HMDUC (28/07/2023)
     */
    confirmAssetChose() {
      this.arrSeleted = this.listAssetChoseSelected;
      this.listAssetTranferChose.data.push(...this.arrSeleted);
      this.arrListId = this.listAssetTranferChose.data;
      this.dataList = {
        data: this.arrListId,
        totalRow: this.arrListId.length,
      };
      this.setListAssetTranferChose(this.dataList);
      this.setListChoseSelected([]);
      this.closeFormChoseAsset();
    },

    /**
     * Function close Modal Detail
     * Author: HMDUC (28/07/2023)
     */
    handleCloseModal() {
      this.isShowPopup = true;
    },

    /**
     * Function confirm close when popup appear
     * Author: HMDUC (28/07/202)
     */
    handleCancel() {
      this.$emit("closeModal");
      this.isShowPopup = false;
    },

    /**
     * Function close open form detail
     * Author: HMDUC (28/07/2002)
     */
    openFormChose() {
      this.isShowFormAsset = true;
      this.ids =
        this.arrListId.length === 0
          ? []
          : this.arrListId.map((item) => item.AssetId);
      this.getListAssetChose({
        ids: this.ids,
        pageSize: this.pageSizeChose,
        pageNumber: this.currentPageChose,
      });
    },

    /**
     * Function close form chose asset
     * Author: HMDUC (28/07/2002)
     */
    closeFormChoseAsset() {
      this.isShowFormAsset = false;
    },

    /**
     * Function next page TableChose
     * Author: HMDUC  (28/07/2023)
     */
    nextPage(page) {
      this.currentPageChose = page;
      this.getListAssetChose({
        ids: this.ids,
        pageSize: this.pageSizeChose,
        pageNumber: this.currentPageChose,
      });
    },

    /**
     * Function prevPage TableChose
     * Author: HMDUC  (28/07/2023)
     */
    prevPage(page) {
      this.currentPageChose = page;
      this.getListAssetChose({
        ids: this.ids,
        pageSize: this.pageSizeChose,
        pageNumber: this.currentPageChose,
      });
    },

    /**
     * Function chosePage Size TableChose
     * Author: HMDUC  (28/07/2023)
     */
    chosePageSize(pageSize) {
      this.pageSizeChose = pageSize;
      this.getListAssetChose({
        ids: this.ids,
        pageSize: this.pageSizeChose,
        pageNumber: 1,
      });
    },
  },
};
</script>

<style scoped>
@import "@/css/base/modal.css";
.table__view {
  justify-content: unset;
  padding: unset;
  height: 100%;
}
</style>
