module.exports = {
  lintOnSave: 'error',
  // options...
  devServer: {
    host: 'localhost',
    port: 8080,
    allowedHosts: [
      'localhost'
    ]
  },
  css: {
    loaderOptions: {
      // pass options to sass-loader
      sass: {
        // @/ is an alias to src/
        // so this assumes you have a file named `src/variables.scss`
        data: `@import "@/styles/application.scss";`
      }
    }
  }
}
