const { env } = require('process');

const target = env.ASPNETCORE_HTTPS_PORT ? `https://localhost:${env.ASPNETCORE_HTTPS_PORT}` :
  env.ASPNETCORE_URLS ? env.ASPNETCORE_URLS.split(';')[0] : 'http://localhost:3467';

const PROXY_CONFIG = [
  {
    context: [
      "/chatbot/Message", "/chatbot/StartChat", "/chatbot/EndChat",
      "/Course/AddCourses", "/Course/GetCourses",
      "/User/CreateUser", "/User/AddCourseToUser", "/User/GetUserCourses"
   ],
    target: target,
    secure: false,
    headers: {
      Connection: 'Keep-Alive'
    }
  }
]

module.exports = PROXY_CONFIG;
