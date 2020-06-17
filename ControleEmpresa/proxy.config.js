const proxy = [
    {
      context: '/api',
      target: 'https://localhost:44331',
      secure: false,
      pathRewrite: {'^/api' : ''}
    }
  ];
  module.exports = proxy;