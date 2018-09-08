module.exports = {
  webpack: (config) => {
    config.stats = 'errors-only'

    return config
  }
};