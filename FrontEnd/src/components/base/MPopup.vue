<!-- eslint-disable no-debugger -->
<template>
  <div class="popup__container">
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
                  dataPopup.length < 10
                    ? `0${dataPopup.length}`
                    : dataPopup.length
                }}</span
              >
              {{ this.$_MISAResources.popup.delete_more }}
            </div>
          </template>
          <template v-else-if="dataPopup.length === 1">
            <div class="property">
              {{ this.$_MISAResources.popup.delete_only }}
              <b>{{ dataPopup[0].AssetCode }}</b>
              -
              <b>
                {{ dataPopup[0].AssetName }}
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
              <span>{{ this.$_MISAResources.popup.add__cancel }}</span>
            </div>
          </template>
          <template v-else>
            <div>
              <span>{{ this.$_MISAResources.popup.edit__cancel }}</span>
            </div>
          </template>
        </template>
      </div>
      <div class="popup__btn">
        <template v-if="type === 'confirm'">
          <m-button
            :content="this.$_MISAResources.content__button.cancel_popup"
            className="btn--sm-pop btn__cancel"
            @click="handleBtnCancel"
          ></m-button>
          <m-button
            :content="this.$_MISAResources.content__button.delete"
            className="btn--sm-pop btn__main"
            @click="handleDelete(dataPopup)"
          ></m-button>
        </template>
        <template v-else-if="type === 'warning'">
          <template
            v-if="this.formMode === this.$_MISAResources.form__mode.edit"
          >
            <m-button
              :content="this.$_MISAResources.content__button.cancel_popup"
              className="btn--sm-pop btn__cancel"
              @click="handleBtnCancel"
            ></m-button>
            <m-button
              :content="this.$_MISAResources.content__button.no_save"
              className="btn--sm-pop btn__outline-pri"
              @click="handleCloseForm()"
            ></m-button>
            <m-button
              :content="this.$_MISAResources.content__button.save"
              className="btn--sm-pop btn__main"
              @click="handleSave(dataPopup)"
            ></m-button>
          </template>
          <template v-else>
            <m-button
              :content="this.$_MISAResources.content__button.no"
              className="btn--sm-pop btn__cancel"
              @click="handleBtnCancel"
            ></m-button>
            <m-button
              :content="this.$_MISAResources.content__button.cancel_popup"
              className="btn--sm-pop btn__main"
              @click="handleCloseForm()"
            ></m-button>
          </template>
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
  props: ["dataPopup", "icon", "isShow", "type"],
  components: {
    "m-button": MButton,
  },
  computed: {
    ...mapState("property", ["isLoading", "propertyList", "listSelected"]),
    ...mapState("formDialog", ["formMode"]),
  },

  updated() {
    this.isShowPopup = this.isShow;
  },
  data() {
    return {
      isShowPopup: this.isShow,
    };
  },
  methods: {
    ...mapActions("property", [
      "delete",
      "setListSelected",
      "deleteMulti",
      "updateProperty",
    ]),
    /**
     * function emit close form when click cancel
     * Author: HMDUC(27/05/2023)x
     */
    handleBtnCancel() {
      this.$emit("cancel");
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
        const res = await this.updateProperty(assetUpdateDto);
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
    async handleDelete(data) {
      let lenghtList = data.length;
      switch (lenghtList) {
        case 1:
          try {
            const res = await this.delete(data[0].AssetId);
            const deleteIndex = this.listSelected.findIndex(
              (item) => item.AssetId == data[0].AssetId
            );
            this.listSelected.splice(deleteIndex, 1);
            if (res === Enum.REQ__CODE.NO_CONTENT) {
              this.$emit("showToast", "success");
              this.$emit("reLoad");
            } else if (res === Enum.REQ__CODE.BAD_REQUEST) {
              this.$emit("showToast", "err");
            }
          } catch (err) {
            this.$emit("showToast", "err");
          }
          break;
        default:
          try {
            let listId = [];
            data.forEach((item) => {
              listId.push(item.AssetId);
            });
            const res = await this.deleteMulti(listId);
            const deleteIndex = [];

            // remove asset in store
            this.listSelected.forEach((item, index) => {
              if (listId.includes(item.AssetId)) {
                deleteIndex.push(index);
              }
            });

            deleteIndex.reverse().forEach((index) => {
              this.listSelected.splice(index, 1);
            });

            if (res === Enum.REQ__CODE.NO_CONTENT) {
              this.$emit("showToast", "success");
            } else if (res === Enum.REQ__CODE.BAD_REQUEST) {
              this.$emit("showToast", "err");
            }
          } catch (err) {
            this.$emit("showToast", "err");
          }
          break;
      }
    },
  },
};
</script>

<style scoped>
@import url(@/css/base/popup.css);
</style>
