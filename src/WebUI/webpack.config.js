const webpack = require('webpack');
const { merge } = require('webpack-merge');
const BundleAnalyzerPlugin = require('webpack-bundle-analyzer').BundleAnalyzerPlugin;
const path = require('path');

var config = {
    entry: {
        'whatbug': './Assets/Scripts/whatbug',
        'home': './Assets/Scripts/Pages/home',
        'dashboard': './Assets/Scripts/Pages/dashboard',
        'kanban': './Assets/Scripts/Pages/kanban',
        'create-edit-project': './Assets/Scripts/Pages/create-edit-project',
        'permission-schemes': './Assets/Scripts/Pages/permission-schemes',
        'roles-and-permissions': './Assets/Scripts/Pages/roles-and-permissions',
        'create-edit-priority': './Assets/Scripts/Pages/create-edit-priority',
        'priorities': './Assets/Scripts/Pages/priorities',
        'create-edit-priority-scheme': './Assets/Scripts/Pages/create-edit-priority-scheme',
        'priority-schemes': './Assets/Scripts/Pages/priority-schemes',
        'project-roles': './Assets/Scripts/Pages/project-roles',
        'assign-users-to-role': './Assets/Scripts/Pages/assign-users-to-role',
        'user-permissions': './Assets/Scripts/Pages/user-permissions'
    },
    output: {
        filename: '[name].js',
        path: path.resolve(__dirname, 'wwwroot/js'),
        clean: true
    },
    externals: {
        jquery: 'jQuery',
    },
    resolve: {
        extensions: [".js"],
        modules: ["node_modules"]
    },
    plugins: [
        new webpack.ProvidePlugin({
            $: "jquery",
            jQuery: "jquery",
        }),
    ],
    module: {
        rules: [
            {
                test: /\.m?js$/,
                exclude: /(node_modules|bower_components)/,
                use: {
                    loader: 'babel-loader',
                    options: {
                        presets: ['@babel/preset-env']
                    }
                }
            }
        ]
    }
};

module.exports = (env, argv) => {
    if (argv.mode === 'development') {
        config = merge(config, {
            devtool: 'source-map',
            watch: true,
            plugins: [
                // new BundleAnalyzerPlugin()
            ]
        });
    }

    return config;
}