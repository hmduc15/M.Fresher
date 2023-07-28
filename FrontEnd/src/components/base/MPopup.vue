<template>
  <div class="popup__container" v-if="isShowPopup">
    <div class="popup__icon" :class="icon"></div>
    <div class="popup__content">
      <div
        :class="[
          'popup__text',
          this.formMode === this.$_MISAResources.form__mode.edit
            ? 'mwidth--345'
            : 'mwidth--300',
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
            @click="handleCloseForm()"
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
  ],
  components: {
    "m-button": MButton,
  },
  data() {
    return {
      isShowPopup: this.isShow,
      dataDelete: this.dataPopup,
    };
  },
  computed: {
    ...mapState("asset", ["isLoading", "assetList", "listSelected"]),
    ...mapState("formDialog", ["formMode"]),
    ...mapState("inputError", ["listError"]),
  },

  updated() {
    this.isShowPopup = this.isShow;
  },

  methods: {
    ...mapActions("asset", [
      "delete",
      "setListSelected",
      "deleteMulti",
      "updateAsset",
    ]),
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
     * function confirm delete property selected
     * Author: HMDUC(27/05/2023)
     */
    handleDelete(data) {
      let lengthList = data.length;
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
     * Function delete more
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
          this.$emit("showToast", "success");
          break;
        case Enum.REQ__CODE.BAD_REQUEST:
          this.$emit("showToast", "err");
          break;
        case Enum.REQ__CODE.ERROR:
          this.$emit("showToast", "err");
          break;
      }
    },
  },
};
</script>

<style scoped>
@import url(@/css/base/popup.css);
</style>
