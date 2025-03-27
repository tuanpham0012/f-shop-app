<template lang="">
    <div class="loading-container">
        <div class="loader"></div>
    </div>
    
</template>
<style lang="scss" scoped>
    /* HTML: <div class="loader"></div> */
.loading-container{
    width: 100%;
    height: 100%;
    display: inline-flex;
    justify-content: center;
    align-items: center;
    background-color: #e2e2e241;
}
/* HTML: <div class="loader"></div> */
/* HTML: <div class="loader"></div> */
/* HTML: <div class="loader"></div> */
.loader {
  width: fit-content;
  font-size: 40px;
  line-height: 1.5;
  font-family: system-ui,sans-serif;
  font-weight: bold;
  text-transform: uppercase;
  color: #0000;
  -webkit-text-stroke: 1px #000;
  background:
    radial-gradient(1.13em at 50% 1.6em,#000 99%,#0000 101%) calc(50% - 1.6em) 0/3.2em 100% text,
    radial-gradient(1.13em at 50% -0.8em,#0000 99%,#000 101%) 50% .8em/3.2em 100% repeat-x  text;
  animation: l9 2s linear infinite;
}
.loader:before {
  content: "Loading";
}
@keyframes l9 {
  to {background-position: calc(50% + 1.6em) 0,calc(50% + 3.2em) .8em}
}
</style>