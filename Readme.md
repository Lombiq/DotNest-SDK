# DotNest SDK readme



## Overview

[DotNest SDK](https://github.com/Lombiq/DotNest-SDK) is a local developer environment for building Media Themes to be deployed on sites running on [DotNest](http://dotnest.com). The base of the source code on the `master` branch is the same Orchard version that is running on DotNest as well as all the hotfixes and mods we've applied to it. On top of that, all the open-source modules and themes are added as Git submodules, which gives you the ability to develop your theme and run your site locally in an environment quite close to the live DotNest site.

This project (along with [DotNest](https://dotnest.com), the [Orchard CMS](http://orchardproject.net) SaaS) is developed by [Lombiq Technologies Ltd](https://lombiq.com). Commercial-grade support is available through Lombiq.


## Getting started

- Go to GitHub and fork the [DotNest SDK](https://github.com/Lombiq/DotNest-SDK) repository or create an empty repository. For simplicity, we'll refer to your repository as `fork` from now on and assume a simple branching strategy with only one additional branch for development, but your use-case might be more complex.
- Set up a mirror on [Git-Hg Mirror](https://githgmirror.com) to automatically (and continuously) synchronize every commit from the original repository to your fork. This gives you an easy way to always work with same code base as what is running on DotNest.
  - The `Git clone URL` should be `git+https://github.com/Lombiq/DotNest-SDK.git`.
  - The `Hg clone URL` (don't worry about `Hg`) should be a similar URL pointing to your fork with some authentication details to allow Git-Hg Mirror to push to your repository. You create an access token under under [your GitHub settings](https://github.com/settings/tokens) (select full `repo` access) and use it as follows: `git+https://0123456789abcdef0123456789abcdef:x-oauth-basic@github.com/AwesomeDeveloper/Awesome-Project.git`.
  - The `Mirroring direction` should be `Git to Hg`.
  - Make sure that you never commit anything on the branches coming from the original repository, otherwise the synchronization will fail.


### Alternative: Hg instead of Git

- By setting up the mirror in a slightly different way, you can also use Mercurial for your project. In that case the authentication details are defined in the `username:password` format (make sure that the user has write access to the repository).
- Note that the [DotNest SDK is also available as a Mercurial repository on Bitbucket](https://bitbucket.org/Lombiq/dotnest-sdk), but the Git and Hg repositories of the DotNest SDK are mirrored bi-directionally, so it doesn't matter which one you're synchronizing from.
- You need to enable the `hggit` extension to be able to checkout the Git submodules in a Mercurial repository.


## Working with the repository 

- Whenever you create any branches, make sure that you prefix their names so they don't collide with the ones in the SDK. For example, if the project you're working is called `Awesome Project`, then your development branch should be created on top of `dev` and name it e.g. `ap-dev`.
- You might also want to change the default branch of your fork to your development branch.
- In case new commits are pushed to your fork from the original repository, check the changes (e.g. new modules might be added that you also need to add to your custom solution) and merge `dev` to your development branch.


## Theme development

- Create a custom solution file on your prefixed development branch as a copy of `DotNest.SDK.sln`, e.g. `AwesomeProject.sln`. Your custom projects should be added to this solution, not `Orchard.sln` or `DotNest.SDK.sln`.
- From here on, general Orchard theme development rules apply with some DotNest-related extra. You can read about all these on the [`Theming a DotNest site` page of the DotNest Knowledge Base](https://dotnest.com/knowledge-base/topics/theming/).
- If your theme contains Liquid templates, enable the `Liquid Markup View Engine` feature for these to be picked up by Orchard.
- The Media Theme on DotNest also has an automated mechanism to include some site-level resources. This might come in handy e.g. if your theme doesn't have a base theme and/or you're not overriding the `Document` or `Layout` shapes. You can enable the same functionality by enabling the `DotNest SDK` feature, which will automatically include the following resources on every page load (if they are available in the active theme): `favicon.ico` in the `Images` folder, `site.css` in the `Styles` folder, and `site-head.js` and `site-foot.js` in the `Scripts` folder.
- You can synchronize content from your site running on DotNest by exporting it and then importing it locally after enabling the `Import-Export` feature.


## Help us make it better!

In case you come across an Orchard bug during development, don't keep it to yourself: Orchard bugs should be reported at [the official Orchard GitHub repository](https://github.com/OrchardCMS/Orchard). There is a chance though that the problem you've discovered is already fixed since the latest release and we could add the necessary changes as a hotfix to DotNest (and the DotNest SDK) to improve it. You can tell us about it by opening an issue at [the DotNest SDK GitHub repository](https://github.com/Lombiq/DotNest-SDK). Thanks!