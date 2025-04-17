<script setup>
import {} from "vue";
/** declare variable */
const props = defineProps({
    title: {
        type: [String, Number],
        default: "",
    },
    modalSize: {
        type: String,
        default: "",
    },
});
const emits = defineEmits(["close"]);
/** declare function */
const closeModal = () => {
    emits("close");
};
</script>

<template>
    <div
        class="modal"
        id="staticBackdrop"
        data-bs-backdrop="static"
        data-bs-keyboard="false"
        tabindex="-1"
    >
        <div
            class="modal-dialog modal-dialog-centered modal-dialog-scrollable"
            :class="modalSize"
        >
            <div class="modal-content">
                <div class="modal-header border-b border-b-gray-400">
                    <h5 class="modal-title">{{ props.title }}</h5>
                    <button type="button" class="btn-close text-white" data-bs-dismiss="modal" aria-label="Close" @click="closeModal()"></button>
                </div>
                <div class="modal-body">
                    <slot name="body"></slot>
                </div>
                <div class="modal-footer">
                    <slot name="footer"></slot>
                </div>
            </div>
        </div>
    </div>
</template>

<style scoped lang="scss">
.modal {
    display: block;
    --bs-modal-zindex: 10;
    --bs-modal-width: 500px;
    --bs-modal-padding: 1rem;
    --bs-modal-margin: 0.5rem;
    --bs-modal-color: rgba(124, 124, 125, 0.5);
    --bs-modal-bg: var(--bs-body-bg);
    --bs-modal-border-color: var(--bs-border-color-translucent);
    --bs-modal-border-width: var(--bs-border-width);
    --bs-modal-border-radius: var(--bs-border-radius-lg);
    --bs-modal-box-shadow: var(--bs-box-shadow-sm);
    --bs-modal-inner-border-radius: calc(
        var(--bs-border-radius-lg) - (var(--bs-border-width))
    );
    --bs-modal-header-padding-x: 1rem;
    --bs-modal-header-padding-y: 1rem;
    --bs-modal-header-padding: 1rem 1rem;
    --bs-modal-header-border-color: var(--bs-border-color);
    --bs-modal-header-border-width: var(--bs-border-width);
    --bs-modal-title-line-height: 1.5;
    --bs-modal-footer-gap: 0.5rem;
    --bs-modal-footer-bg: ;
    --bs-modal-footer-border-color: var(--bs-border-color);
    --bs-modal-footer-border-width: var(--bs-border-width);
    background-color: var(--bs-modal-color);
    z-index: 10;
}

.modal-body
{
    padding: 0;
}

// .loading-spinner {
//     width: 30px;
//     height: 30px;
//     border: 2px solid indigo;
//     border-radius: 50%;
//     border-top-color: #0001;
//     display: inline-block;
//     animation: loadingspinner 0.7s linear infinite;
// }
// @keyframes loadingspinner {
//     0% {
//         transform: rotate(0deg);
//     }
//     100% {
//         transform: rotate(360deg);
//     }
// }
</style>
