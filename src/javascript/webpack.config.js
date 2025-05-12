const path = require('path');
const webpack = require('webpack');

const bundleFileName = 'bundle';
const dirName = '../dotnet/Blazor3D/Blazor3D/wwwroot/js';

module.exports = (env, argv) => {
    const isProd = argv.mode === "production";

    return {
        mode: isProd ? "production" : "development",
        entry: ['./index.js'],
        output: {
            library: {
                type: 'module'
            },
            umdNamedDefine: true,
            filename: isProd ? bundleFileName + ".min.js" : bundleFileName + ".js",
            path: path.resolve(__dirname, dirName),
        },
        plugins: [
            new webpack.BannerPlugin({
                banner: `Copyright © 2022 Roman Simuta aka siroman \nCopyright © 2010-2021 three.js authors https://threejs.org/`
            })
        ],
        experiments: {
            outputModule: true,
        },
        optimization: {
            minimize: isProd // only minimize in production
        },
        devtool: isProd ? false : 'source-map', // keep source maps for debugging in development
    };
};