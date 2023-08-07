<template>
  <div class="popup__container" v-if="isShowPopup">
    <div class="popup__icon" :class="icon"></div>
    <div
      class="popup__content"
      :class="[`${type === 'dele__error' ? 'popup--detail' : ''}`]"
    >
      <div
        :class="[
          'popup__text',
          this.formMode === this.$_MISAResources.form__mode.edit
            ? 'mwidth--345'
            : 'mwidth--300',
          `${
            type === 'dele__error' || type === 'asset__dele-err'
              ? 'mwidth--380'
              : ''
          }`,
        ]"
      >
        <template v-if="type === 'confirm'">
          <template v-if="dataPopup.length > 1">
            <div>
              <span style="font-weight: bold">
                {{
                  dataDelete.length < 10
                    ? `0${dataDelete.length}`
                    : dataDelete.length
                }}</span
              >
              {{ this.$_MISAResources.popup.deleteMore }}
            </div>
          </template>
          <template v-else-if="dataDelete.length === 1">
            <div class="property">
              {{ this.$_MISAResources.popup.deleteOnly }}
              <b>{{ dataDelete[0].AssetCode }}</b>
              -
              <b>
                {{ dataDelete[0].AssetName }}
                ?
              </b>
            </div>
          </template>
        </template>
        <template v-else-if="type === 'warning'">
          <template
            v-if="
              this.formMode === this.$_MISAResources.form__mode.add ||
              this.formMode === this.$_MISAResources.form__mode.duplicate
            "
          >
            <div>
              <span>{{ this.$_MISAResources.popup.addCancel }}</span>
            </div>
          </template>
          <template
            v-else-if="this.formMode === this.$_MISAResources.form__mode.edit"
          >
            <div>
              <span>{{ this.$_MISAResources.popup.editCancel }}</span>
            </div>
          </template>
        </template>
        <template v-else-if="type === 'warning__modal'">
          <div>
            <span>{{ this.$_MISAResources.popup.addReceipt }}</span>
          </div>
        </template>
        <template v-else-if="type === 'error'">
          <template v-if="errMessage.length > 1">
            <ul v-for="(item, index) in errMessage" :key="index">
              <li class="error__item">{{ item }}</li>
            </ul>
          </template>
          <template v-else-if="errMessage.length === 1">
            <div>{{ errMessage[0] }}</div>
          </template>
        </template>
        <template v-else-if="type === 'no__assetChose'">
          <div>
            <span>{{ this.$_MISAResources.popup.choseAsset }}</span>
          </div>
        </template>
        <template v-else-if="type === 'no__departmentNew'">
          <div>
            <span>{{ this.$_MISAResources.popup.noDepartmentReceipt }}</span>
          </div>
        </template>
        <template v-else-if="type === 'exist__department'">
          <div>
            <span>{{ this.$_MISAResources.popup.existDepartment }}</span>
          </div>
        </template>
        <template v-else-if="type === 'confirm__receipt'">
          <template v-if="dataPopup.length > 1">
            <div>
              <span style="font-weight: bold">
                {{
                  dataPopup.length < 10
                    ? `0${dataPopup.length}`
                    : dataPopup.length
                }}</span
              >
              {{ this.$_MISAResources.popup.deleteMoreReceipt }}
            </div>
          </template>
          <template v-else>
            <div class="property">
              {{ this.$_MISAResources.popup.deleteOnlyRecipt }}
              <b>{{ dataDelete[0].ReceiptCode }}</b>
              {{ this.$_MISAResources.popup.noFreeText }}
            </div>
          </template>
        </template>
        <template v-else-if="type === 'dupli_depart'">
          <div>
            <span>{{ this.$_MISAResources.popup.dupliDepart }}</span>
          </div>
        </template>
        <template v-else-if="type === 'receipt__date'">
          <div>
            <span>{{ this.$_MISAResources.popup.receiptDate }}</span>
          </div>
        </template>
        <template
          v-else-if="type === 'dele__error' || type === 'asset__dele-err'"
        >
          <div>
            {{ this.$_MISAResources.popup.asset }}
            <b>{{ assetError.AssetCode }}</b>
            <template v-if="type === 'dele__error'">
              {{ this.$_MISAResources.popup.accuredReceipt }}
            </template>
            <template v-else>
              {{ this.$_MISAResources.popup.assetReceipt }}
            </template>
          </div>
          <div class="view__detail" v-if="isShowDetail">
            <p class="text__action" @click="toogleDetail">
              {{ this.$_MISAResources.content__button.viewDetail }}
            </p>
          </div>
          <div class="detail" v-if="!isShowDetail">
            <p class="text__action" @click="toogleDetail">
              {{ this.$_MISAResources.content__button.hideDetail }}
            </p>
            <div v-for="(item, index) in this.receiptError" :key="index">
              {{ this.$_MISAResources.popup.receipt }}
              <b>{{ item.receiptCode }} ({{ item.receiptDate }})</b>
            </div>
          </div>
        </template>
        <templat v-else-if="type === 'no__member'">
          <div>
            <span>{{ this.$_MISAResources.popup.noMember }}</span>
          </div>
        </templat>
      </div>
      <div class="popup__btn">
        <template v-if="type === 'confirm'">
          <m-button
            :content="this.$_MISAResources.content__button.cancelPopup"
            className="btn--sm-pop btn__cancel"
            @click="handleBtnCancel"
            :tabindex="1"
          ></m-button>
          <m-button
            :content="this.$_MISAResources.content__button.delete"
            className="btn--sm-pop btn__main"
            @click="handleDelete(dataDelete)"
            :tabindex="2"
          ></m-button>
        </template>
        <template v-if="type === 'confirm__receipt'">
          <m-button
            :content="this.$_MISAResources.content__button.cancelPopup"
            className="btn--sm-pop btn__cancel"
            @click="handleBtnCancel"
            :tabindex="1"
          ></m-button>
          <m-button
            :content="this.$_MISAResources.content__button.delete"
            className="btn--sm-pop btn__main"
            @click="handleDelete(dataDelete)"
            :tabindex="2"
          ></m-button>
        </template>
        <template v-else-if="type === 'warning'">
          <template
            v-if="this.formMode === this.$_MISAResources.form__mode.edit"
          >
            <m-button
              :content="this.$_MISAResources.content__button.cancelPopup"
              className="btn--sm-pop btn__cancel"
              @click="handleBtnCancel"
            ></m-button>
            <m-button
              :content="this.$_MISAResources.content__button.noSave"
              className="btn--sm-pop btn__outline-pri"
              @click="handleCloseForm"
            ></m-button>
            <m-button
              :content="this.$_MISAResources.content__button.save"
              className="btn--sm-pop btn__main"
              @click="handleSave(dataForm)"
            ></m-button>
          </template>
          <template v-else>
            <m-button
              ref="btnCancel"
              :content="this.$_MISAResources.content__button.no"
              className="btn--sm-pop btn__cancel"
              tabindex="1"
              @click="handleBtnCancel"
            ></m-button>
            <m-button
              :content="this.$_MISAResources.content__button.cancelPopup"
              className="btn--sm-pop btn__main"
              @click="handleCloseForm()"
              tabindex="2"
            ></m-button>
          </template>
        </template>
        <template v-else-if="type === 'warning__modal'">
          <m-button
            ref="btnCancel"
            :content="this.$_MISAResources.content__button.no"
            className="btn--sm-pop btn__cancel"
            tabindex="1"
            @click="handleBtnCancel"
          ></m-button>
          <m-button
            :content="this.$_MISAResources.content__button.yes"
            className="btn--sm-pop btn__main"
            @click="handleAgree"
            tabindex="2"
          ></m-button>
        </template>
        <template v-else-if="type === 'error'">
          <m-button
            :content="this.$_MISAResources.content__button.agree"
            className="btn--sm-pop btn__main"
            @click="handleClosePop"
            :tabindex="2"
          ></m-button>
        </template>
        <template
          v-else-if="
            type === 'no__assetChose' ||
            type === 'no__departmentNew' ||
            type === 'exist__department' ||
            type === 'dele__error' ||
            type === 'no__member' ||
            type === 'asset__dele-err'
          "
        >
          <m-button
            :content="this.$_MISAResources.content__button.agree"
            className="btn--sm-pop btn__main"
            @click="handleClosePop"
            :tabindex="2"
          ></m-button>
        </template>
        <template
          v-else-if="type === 'dupli_depart' || type === 'receipt__date'"
        >
          <m-button
            :content="this.$_MISAResources.content__button.agree"
            className="btn--sm-pop btn__main"
            @click="handleClosePop"
            :tabindex="2"
          ></m-button>
        </template>
      </div>
    </div>
  </div>
</template>

<script>
import MButton from "./MButton.vue";
import Enum from "@/utils/enum";

import { mapActions, mapState } from "vuex";

export default {
  name: "MPopup",
  props: [
    "dataPopup",
    "icon",
    "isShow",
    "type",
    "dataForm",
    "errMessage",
    "content",
    "assetError",
  ],
  components: {
    "m-button": MButton,
  },
  data() {
    return {
      isShowPopup: this.isShow,
      dataDelete: this.dataPopup,
      isShowDetail: true,
    };
  },
  computed: {
    ...mapState("asset", ["isLoading", "assetList", "listSelected"]),
    ...mapState("receipt", ["listReceiptSelected", "receiptError"]),
    ...mapState("formDialog", ["formMode"]),
    ...mapState("inputError", ["listError"]),
  },

  updated() {
    this.isShowPopup = this.isShow;
  },

  mounted() {},

  methods: {
    ...mapActions("asset", [
      "delete",
      "setListSelected",
      "deleteMulti",
      "updateAsset",
    ]),
    ...mapActions("receipt", ["deleteOnlyReceipt", "listReceiptSelected"]),
    ...mapActions("inputError", ["setListError"]),

    toogleDetail() {
      this.isShowDetail = !this.isShowDetail;
    },

    /**
     * function emit close form when click cancel
     * Author: HMDUC(27/05/2023)
     */
    handleBtnCancel() {
      this.$emit("cancel");
      if (this.type === "confirm") {
        this.setListSelected([]);
      }
    },

    /**
     * Function emit close popup error
     * Author: HMDUC (15/05/2023)
     */
    handleClosePop() {
      var firstError = null;
      this.$emit("cancel");
      for (var input of this.listError) {
        if (!firstError) {
          firstError = input;
        }
        input.isError = true;
      }
      if (firstError) {
        this.$nextTick(() => {
          firstError.handleFocus();
        });
      }
      this.setListError([]);
    },

    handleAgree() {
      this.$emit("agree");
    },

    /**
     * Function emit cancel form
     * Author: HMDUC(27/05/2023)
     */
    handleCloseForm() {
      this.$emit("closeForm");
    },

    /**
     * Funtion confirm save edit Asset In Form Edit
     * Author: HMDUC (29/06/2023)
     * @param {*} data
     */
    async handleSave(data) {
      var assetUpdateDto = {
        AssetId: data.AssetId,
        AssetCode: data.AssetCode,
        AssetName: data.AssetName,
        DepartmentId: data.DepartmentId,
        DepreciationRate: data.DepreciationRate,
        CategoryId: data.CategoryId,
        PurchaseDate: data.PurchaseDate,
        Cost: data.Cost,
        Quantity: data.Quantity,
        LifeTime: data.LifeTime,
        ProductionYear: data.ProductionYear,
        TrackedYear: data.TrackedYear,
      };

      try {
        const res = await this.updateAsset(assetUpdateDto);
        if (res.status === Enum.REQ__CODE.SUCCESS) {
          this.$emit("showToast", "success__update");
          this.handleCloseForm();
        }
      } catch (err) {
        this.$emit("showToast", "err__add");
      }
    },

    /**
     * function confirm delete asset selected
     * Author: HMDUC(27/05/2023)
     */
    handleDelete(data) {
      let lengthList = data.length;
      switch (this.type) {
        case "confirm":
          switch (lengthList) {
            case 1:
              this.deleteOne(data);
              this.isShowPopup = false;
              break;
            default:
              this.deleteMore(data);
              break;
          }
          this.listSelected.length = 0;
          break;
        case "confirm__receipt":
          switch (lengthList) {
            case 1:
              this.deleteOneReceipt(data);
              this.isShowPopup = false;
              break;
          }
          this.listReceiptSelected.length = 0;
          break;
      }
    },

    /**
     * Function delete one receipt
     * Author: HMDUC (31/07/2023)
     * @param {*} data
     */
    async deleteOneReceipt(data) {
      try {
        const res = await this.deleteOnlyReceipt(data[0].ReceiptId);
        this.handleToastResponse(res);
      } catch (err) {
        this.handleToastResponse(500);
      }
    },

    /**
     * Function delete one asset
     * Author: MDUC(27/05/2023)
     * @param {*} data
     */
    async deleteOne(data) {
      try {
        const res = await this.delete(data[0].AssetId);
        this.handleToastResponse(res);
      } catch (err) {
        this.handleToastResponse(500);
      }
    },

    /**
     * Function delete more asset
     * Author: MDUC(27/05/2023)
     * @param {*} data
     */
    async deleteMore(data) {
      try {
        let listId = [];
        data.forEach((item) => {
          listId.push(item.AssetId);
        });
        const res = await this.deleteMulti(listId);
        this.handleToastResponse(res);
      } catch (err) {
        this.handleToastResponse(500);
      }
    },

    /**
     * Funtion handle request respone
     * Author: HMDUC (03/07/2023)
     * @param {*} code
     */
    handleToastResponse(code) {
      switch (code) {
        case Enum.REQ__CODE.NO_CONTENT:
          if (this.type === "confirm") {
            this.$emit("showToast", "success");
          }
          this.$emit("showToast", "success__receipt");
          break;
        case Enum.REQ__CODE.BAD_REQUEST:
          if (this.type === "confirm") {
            this.$emit("showToast", "err");
          }
          this.$emit("showToast", "err__receipt");
          break;
        case Enum.REQ__CODE.ERROR:
          if (this.type === "confirm") {
            this.$emit("showToast", "err");
          }
          this.$emit("showToast", "err__receipt");
          break;
      }
    },
  },
};
</script>

<style scoped>
@import url(@/css/base/popup.css);
</style>
